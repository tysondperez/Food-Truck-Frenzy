using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class TruckMovement : MonoBehaviour
{
    private Animator anim;
    private new Rigidbody rigidbody;
    
    private float turnSpeed = 3f;
    public float maxSpeed = 40f;
    public float acceleration = 20f;
    public float deceleration = 15f;
    public float currentSpeed = 0f;
    
    [SerializeField] private float turnLerpSpeed = 5f; // Adjust for smoothness
    private float currentDirection = 0f;
    
    /*[SerializeField] private Transform frontRaycastOrigin;
    [SerializeField] private Transform backRaycastOrigin;
    [SerializeField] private float raycastDistance = 2f;
    [SerializeField] private float tiltSmoothness = 5f;*/
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        float targetTurnInput = Input.GetAxis("Horizontal");
        currentDirection = Mathf.Lerp(currentDirection, targetTurnInput, Time.deltaTime * turnLerpSpeed);
        anim.SetFloat("Direction", currentDirection);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        if (moveVertical > 0) // Forward
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (moveVertical < 0) // Braking or reverse
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else // Decelerate when no input
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= (deceleration / 2f) * Time.deltaTime;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += (deceleration / 2f) * Time.deltaTime;
            }
        }
        // Clamp the current speed to avoid exceeding max speed or going below 0 (for forward movement)
        currentSpeed = Mathf.Clamp(currentSpeed, -(maxSpeed / 4f), maxSpeed);

        Vector2 moveInput = new Vector2 (0f, moveVertical); //include horizontal if you want strafe
        
        if (currentSpeed > -0.1f && currentSpeed < 0.1f)
        {
            currentSpeed = 0f;
        }
        print(moveInput + " , "+ currentSpeed);
        Vector3 movement = transform.forward * currentSpeed;
        rigidbody.velocity = new Vector3(movement.x, rigidbody.velocity.y, movement.z);

        Vector3 rotation = new Vector3(0f, moveHorizontal, 0f); //exclude this if you want strafe instead of rotate
        Quaternion deltaRotation = Quaternion.Euler(rotation * (Input.GetKey(KeyCode.LeftShift) ? turnSpeed * 1.5f : turnSpeed));
        if (currentSpeed != 0f)
        {
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }

        //DoTiltRaycasts();
    }

    /*private void DoTiltRaycasts()
    {
        // Perform raycasts
        RaycastHit frontHit, backHit;
        bool frontHitSuccess = Physics.Raycast(frontRaycastOrigin.position, Vector3.down, out frontHit, raycastDistance);
        bool backHitSuccess = Physics.Raycast(backRaycastOrigin.position, Vector3.down, out backHit, raycastDistance);

        // If both raycasts hit, calculate tilt
        if (frontHitSuccess && backHitSuccess)
        {
            Vector3 frontPoint = frontHit.point;
            Vector3 backPoint = backHit.point;

            // Calculate tilt direction
            Vector3 tiltDirection = (frontPoint - backPoint).normalized;

            // Calculate target rotation based on tilt
            Quaternion targetRotation = Quaternion.LookRotation(tiltDirection, Vector3.up);

            // Smoothly interpolate to the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * tiltSmoothness);
        }
    }*/
}
