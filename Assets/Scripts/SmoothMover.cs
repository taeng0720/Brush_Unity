using UnityEngine;

public class SmoothMover : MonoBehaviour
{
    public Transform targetObject;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private void Update()
    {
        MoveAndRotateTowardsTarget();
    }

    private void MoveAndRotateTowardsTarget()
    {
        Vector3 direction = (targetObject.position - transform.position);
        direction.y = 0;

        float distance = direction.magnitude;

        if (distance > 0.1f)
        {
            direction.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
