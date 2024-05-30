using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform truck; // 트럭의 Transform
    public float followDistance = 5f; // 트레일러가 트럭 뒤를 따라가는 거리
    public float rotationSpeed = 2f; // 트레일러의 회전 속도

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 lastTruckPosition; // 트럭의 이전 위치

    void Update()
    {
        // 트럭의 전진 방향을 구함
        Vector3 truckForward = truck.forward;

        // 트럭의 위치를 기준으로 트레일러가 따라가야 할 위치 계산
        targetPosition = truck.position - truckForward * followDistance;

        // 트럭의 속도에 따라 따라가야 할 거리를 조정
        float truckSpeed = (truck.position - lastTruckPosition).magnitude / Time.deltaTime;
        float adjustedFollowDistance = followDistance + truckSpeed * Time.deltaTime;

        // 트레일러가 따라가야 할 위치로 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, rotationSpeed);

        // 트럭과 트레일러의 각도를 구함
        Quaternion truckRotation = Quaternion.LookRotation(truckForward);

        // 트레일러가 트럭과 같은 방향으로 회전하도록 보간
        transform.rotation = Quaternion.Slerp(transform.rotation, truckRotation, Time.deltaTime * rotationSpeed);

        // 트럭의 이전 위치 저장
        lastTruckPosition = truck.position;
    }
}
