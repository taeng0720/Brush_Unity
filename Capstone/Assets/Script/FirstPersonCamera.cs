using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform
    public float sensitivity = 2f; // 마우스 감도
    public float maxVerticalAngle = 80f; // 최대 수직 각도
    public float maxHorizontalAngle = 80f; // 최대 수평 각도

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        // 마우스 입력을 받아서 회전 각도 계산
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 수평 회전
        rotationY -= mouseX;
        rotationY = Mathf.Clamp(rotationY, -maxHorizontalAngle, maxHorizontalAngle);
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);

        // 수직 회전
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxVerticalAngle, maxVerticalAngle);
        // 수직 회전을 적용하기 전에 현재 수평 회전을 기준으로 수정된 회전을 계산합니다.
        Quaternion verticalRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.localRotation *= verticalRotation;
    }
}
