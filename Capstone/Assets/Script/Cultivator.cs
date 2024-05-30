using System.Collections;
using TMPro;
using UnityEngine;

public class Cultivator : MonoBehaviour
{
    // 이동 및 회전 관련 변수
    public float moveSpeed = 20f; // 이동 속도
    public float rotationSpeed = 70; // 회전 속도
    public float acceleration = 5f; // 가속도
    private float rotateInput = 0;

    // 속도 및 기어 관련 변수
    public bool speed = true; // 차량 속도
    public GameObject transmission_gear; // 변속 기어 모델
    public GameObject Speed_gear;
    public bool transmission = false;
    public Transform[] gear_points;
    public GameObject Slow_Gear; // 느린 기어 모델
    public GameObject Fast_Gear; // 빠른 기어 모델

    // 기타 조작 관련 변수
    public float breakKey = 1f; // 브레이크 키
    public bool accel = false; // 가속 여부
    public bool choke = false; // 호흡 억제 여부

    // 현재 상태를 저장할 변수
    private float currentMoveSpeed = 0f; // 현재 이동 속도
    private float currentRotationSpeed = 0f; // 현재 회전 속도

    // 바퀴 및 레버 관련 변수
    public Transform frontLeftWheel; // 앞 왼쪽 바퀴
    public Transform frontRightWheel; // 앞 오른쪽 바퀴
    public Transform BackLeftWheel; // 뒤 왼쪽 바퀴
    public Transform BackRightWheel; // 뒤 오른쪽 바퀴
    public GameObject Right_Lever; // 오른쪽 레버
    public GameObject Left_Lever; // 왼쪽 레버
    public GameObject Rotate_Right_Lever; // 회전용 오른쪽 레버
    public GameObject Rotate_Left_Lever; // 회전용 왼쪽 레버
    public GameObject Normal_Right_Lever; // 일반 오른쪽 레버
    public GameObject Normal_Left_Lever; // 일반 왼쪽 레버

    [SerializeField] private TMP_Text SpeedText, GearText, TransmissionText;
    private Rigidbody rb;

    private void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 기어 및 속도 제어
        if (speed)
            GearControls(Fast_Gear, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, 30f, 40f, 50f);
        else
            GearControls(Slow_Gear, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, 3f, 5f, 7.5f);

        // 기어 변경
        if (Input.GetKeyDown(KeyCode.R))
            ChangeGears();

        // 브레이크 설정
        if (Input.GetKeyDown(KeyCode.Space))
            SetBreakKey(0f, 20f);

        if (Input.GetKeyUp(KeyCode.Space))
            SetBreakKey(1f, 10f);

        // 이동 및 회전 처리
        HandleMovement();
        HandleRotation();

        Vector3 velocity = rb.velocity;
        float _speed = velocity.magnitude;
        SpeedText.text = "Speed : " + _speed;
    }

    // 기어 제어 함수
    void GearControls(GameObject gear, KeyCode key1, KeyCode key2, KeyCode key3, float speed1, float speed2, float speed3)
    {
        if (Input.GetKeyDown(key1) && !transmission)
        {
            if (moveSpeed == 0)
            {
                transmission = true;
                StartCoroutine(MoveLever(0));
            }
            if (moveSpeed == 40 || moveSpeed == 5)
            {
                transmission = true;
                StartCoroutine(MoveLever(1));
            }
            if (moveSpeed == 50 || moveSpeed == 7.5f)
            {
                transmission = true;
                StartCoroutine(MoveLever(2));
            }
            moveSpeed = speed1;

            GearText.text = "Gear : 1";

        }
        else if (Input.GetKeyDown(key2) && !transmission)
        {

            if (moveSpeed == 0)
            {
                transmission = true;
                StartCoroutine(MoveLever(3));
            }
            if (moveSpeed == 30 || moveSpeed == 3)
            {
                transmission = true;
                StartCoroutine(MoveLever(4));
            }
            if (moveSpeed == 50 || moveSpeed == 7.5f)
            {
                transmission = true;
                StartCoroutine(MoveLever(5));
            }
            moveSpeed = speed2;

            GearText.text = "Gear : 2";
        }
        else if (Input.GetKeyDown(key3) && !transmission)
        {
            if (moveSpeed == 0)
            {
                transmission = true;
                StartCoroutine(MoveLever(6));
            }
            if (moveSpeed == 30 || moveSpeed == 3)
            {
                transmission = true;
                StartCoroutine(MoveLever(7));
            }
            if (moveSpeed == 40 || moveSpeed == 5)
            {
                transmission = true;
                StartCoroutine(MoveLever(8));
            }
            moveSpeed = speed3;

            GearText.text = "Gear : 3";
        }

        transmission_gear.transform.position = Vector3.Lerp(gear.transform.position, speed ? Fast_Gear.transform.position : Slow_Gear.transform.position, 0.02f);
    }

    IEnumerator MoveLever(int waypointIndex)
    {
        int[] waypoints;

        // 각 waypointIndex에 따른 웨이포인트 배열 설정
        switch (waypointIndex)
        {
            case 0:
                waypoints = new int[] { 4, 0 };
                break;
            case 1:
            case 2:
                waypoints = new int[] { 3, 4, 0 };
                break;
            case 3:
            case 5:
                waypoints = new int[] { 1 };
                break;
            case 4:
                waypoints = new int[] { 4, 3, 1 };
                break;
            case 6:
                waypoints = new int[] { 2 };
                break;
            case 7:
                waypoints = new int[] { 4, 3, 2 };
                break;
            case 8:
                waypoints = new int[] { 3, 2 };
                break;
            default:
                // 다른 추가적인 waypointIndex에 대한 처리 코드 추가 가능
                yield break;
        }

        foreach (int wp in waypoints)
        {
            while (Vector3.Distance(Speed_gear.transform.position, gear_points[wp].transform.position) > 0.01f)
            {
                Speed_gear.transform.position = Vector3.Lerp(Speed_gear.transform.position, gear_points[wp].transform.position, 0.3f);
                yield return null;
            }

            // 일정 시간 대기
            yield return new WaitForSeconds(0.013f);
        }

        transmission = false;
    }


    // 기어 변경 함수
    void ChangeGears()
    {
        speed = !speed;
        switch (moveSpeed)
        {
            case 30:
                moveSpeed = 3;
                break;
            case 40:
                moveSpeed = 5;
                break;
            case 50:
                moveSpeed = 7.5f;
                break;
            case 3:
                moveSpeed = 30;
                break;
            case 5:
                moveSpeed = 40;
                break;
            case 7.5f:
                moveSpeed = 50;
                break;
            default:
                break;
        }
    }


    // 브레이크 설정 함수
    void SetBreakKey(float value, float acc)
    {
        breakKey = value;
        acceleration = acc;
    }

    // 이동 처리 함수
    void HandleMovement()
    {
        currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, breakKey * moveSpeed, acceleration * Time.deltaTime);
        Vector3 moveDirection = transform.forward * currentMoveSpeed;
        transform.position += moveDirection * Time.deltaTime;
    }

    // 회전 처리 함수
    void HandleRotation()
    {
        if (currentMoveSpeed > 2f)
        {
            if (Input.GetKey(KeyCode.Z))
                RotateLever(Rotate_Left_Lever, Left_Lever, 1);
            else if (Input.GetKey(KeyCode.C))
                RotateLever(Rotate_Right_Lever, Right_Lever, 2);
            else
            {
                RotateLever(Normal_Left_Lever, Left_Lever, 0);
                RotateLever(Normal_Right_Lever, Right_Lever, 0);
                rotateInput = 0;
            }

            transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
        }

        float accelerationMultiplier = currentMoveSpeed / moveSpeed;
        float rotateAmount = 2 * rotationSpeed * Time.deltaTime * accelerationMultiplier * acceleration;

        frontLeftWheel.Rotate(Vector3.right, rotateAmount);
        frontRightWheel.Rotate(Vector3.right, rotateAmount);
        BackLeftWheel.Rotate(Vector3.right, rotateAmount);
        BackRightWheel.Rotate(Vector3.right, rotateAmount);
    }

    // 레버 회전 함수
    void RotateLever(GameObject targetLever, GameObject sourceLever, int num)
    {
        Quaternion targetRotation = targetLever.transform.rotation;
        sourceLever.transform.rotation = Quaternion.Lerp(sourceLever.transform.rotation, targetRotation, 5 * Time.deltaTime);
        if (num == 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rotateInput = -1;
            }
        }
        if (num == 2)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rotateInput = 1;
            }
        }

    }
}