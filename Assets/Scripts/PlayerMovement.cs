using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float swingSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float climbSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultipiler;
    public float slopeIncreaseMultipiler;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("References")]
    public Climbing climbingScript;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    private MovementState lastState;
    private bool keepMomentum;

    public TextMeshProUGUI speed_text;
    public TextMeshProUGUI state_text;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallrunning,
        swinging,
        dashing,
        climbing,
        crouching,
        sliding,
        air
    }
    public bool climbing;
    public bool sliding;
    public bool wallrunning;
    public bool dashing;
    public bool swinging;

    public bool freeze;
    public bool unlimited;

    public bool activeGrapple;

    public bool restricted;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        //UI
        speed_text.text = desiredMoveSpeed.ToString();
        state_text.text = state.ToString();
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f,whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();
        //handle drag
        if ((state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching) && !activeGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        //start crouch
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //stop crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
    private void StateHandler()
    {

        //Freeze
        if (freeze)
        {
            state = MovementState.freeze;
            moveSpeed = 0;
            rb.velocity = Vector3.zero;
        }
        //Unlimited
        else if (unlimited)
        {
            state = MovementState.unlimited;
            moveSpeed = 999f;
            return;
        }
        //Dashing
        else if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        //Swinging
        else if (swinging)
        {
            state = MovementState.swinging;
            moveSpeed = swingSpeed;
        }
        //Wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }
        //Climbing
        else if(climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        //Sliding
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;
            else
                desiredMoveSpeed = sprintSpeed;
        }
        //Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        //Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        //Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        //Air
        else
        {
            state = MovementState.air;
            if (desiredMoveSpeed < sprintSpeed)
                desiredMoveSpeed = walkSpeed;
            else
                desiredMoveSpeed = sprintSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if(lastState == MovementState.dashing) keepMomentum = true;

        if(desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }
        {
            
        }
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed !=0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }
        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }
    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float diffrence = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;
        while (time < diffrence)
        {
            moveSpeed = Mathf.Lerp(startValue,desiredMoveSpeed,time / diffrence);
            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up,slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultipiler * slopeIncreaseMultipiler * slopeAngleIncrease;
            }
            else
            {
                time += Time.deltaTime * boostFactor;
            }
            
            yield return null;
        }
        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }
    private void MovePlayer()
    {
        if (activeGrapple) return;
        if (swinging) return;

        if (state == MovementState.dashing) return;

        if (restricted) return;


        if (climbingScript.exitingWall) return;
        //movement direction
        moveDirection = orientation.forward * verticalInput+orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //on ground
        else if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        //air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
        //turn gravity off while on slope
        if(!wallrunning) rb.useGravity = !OnSlope();

    }
    private void SpeedControl()
    {
        if(activeGrapple) return;

        if (OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;

        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        if(maxYSpeed !=0 && rb.velocity.y > maxYSpeed)
            rb.velocity = new Vector3(rb.velocity.x,maxYSpeed, rb.velocity.z);
        
    }
    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x,0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }
    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction,slopeHit.normal).normalized;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x,0f,endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}
