using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class TruckMovement : MonoBehaviour
{
    //private Animator anim;
    private new Rigidbody rigidbody;
    
    private float turnSpeed = 3f;
    public float maxSpeed = 40f;
    public float acceleration = 20f;
    public float deceleration = 15f;
    public float currentSpeed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*anim.SetFloat("Speed", Input.GetAxis("Vertical"));
        anim.SetFloat("Direction", Input.GetAxis("Horizontal"));*/
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
    }
}
