using UnityEngine;
using DG.Tweening;
public class Main_MoveCamera : MonoBehaviour
{
    public Transform targetObject;
    public float moveDuration = 2f;
    public float rotationDuration = 2f;

    private void Start()
    {
        MoveAndRotateCamera();
    }

    private void MoveAndRotateCamera()
    {
        transform.DOMove(targetObject.position, moveDuration).SetEase(Ease.InOutSine);

        transform.DORotateQuaternion(targetObject.rotation, rotationDuration).SetEase(Ease.InOutSine);
    }
}