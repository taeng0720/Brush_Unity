using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swinging : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;
    public PlayerMovement pm;

    [Header("Swinging")]
    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("OdmGear")]
    public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrustForce = 10f;
    public float forwardThrustForce = 15f;
    public float extendCableSpeed = 2f;

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    private void Awake()
    {
        DontDestroyOnLoad(predictionPoint);
        // 씬이 전환될 때 이벤트에 StopSwing 연결
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 씬 로드 이벤트에서 연결 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 시 스윙 초기화
        StopSwing();
    }

    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
        CheckForSwingPoints();
        if (joint != null) OdmGearMovement();
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartSwing()
    {
        if (predictionHit.point == Vector3.zero) return;

        if (GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        pm.ResetRestrictions();

        pm.swinging = true;

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);
        joint.maxDistance = distanceFromPoint * 0.5f;
        joint.minDistance = distanceFromPoint * 0.1f;
        joint.spring = 10f;
        joint.damper = 5f;
        joint.massScale = 4.5f;

        lr.positionCount = 2;
        currentGrapplePosition = gunTip.position;
    }

    private Vector3 currentGrapplePosition;

    public void StopSwing()
    {
        pm.swinging = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    private void OdmGearMovement()
    {
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime * 2f);
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime * 2f);
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime * 2f);

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime * 2f);
            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);
            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;
            joint.maxDistance = extendedDistanceFromPoint * 0.5f;
            joint.minDistance = extendedDistanceFromPoint * 0.1f;
        }
    }

    private void CheckForSwingPoints()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward,
                            out sphereCastHit, maxSwingDistance, whatIsGrappleable);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward,
                            out raycastHit, maxSwingDistance, whatIsGrappleable);

        Vector3 realHitPoint;

        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;
        else
            realHitPoint = Vector3.zero;

        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    private void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}
