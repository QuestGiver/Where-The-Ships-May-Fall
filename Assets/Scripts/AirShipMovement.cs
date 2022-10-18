using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AirShipMovement : MonoBehaviour
{
    [SerializeField]
    float thrust = 10f;//Also a limmit on the abstract concecpt of "engine power availible"
    [SerializeField]
    float acceleration = 10f;//How fast the ship can accelerate
    [SerializeField]
    float throttleInput, throttleMin, throttleMax, throttleCurrent;
    [SerializeField]
    float elevation, elevationMin, elevationMax, elevationCurrent;
    [SerializeField]
    float yaw;
    [SerializeField]
    float rotationTorque = 10f;
    [SerializeField]
    float rotationInput, rotationMin, rotationMax, rotationCurrent;
    [SerializeField]
    Vector3 heading;
    [SerializeField]
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        AdjustThrottle();
        MovementUpdate();
    }

    public void InputHandling(InputAction.CallbackContext context)
    {
            throttleInput = context.ReadValue<float>();
    }

    void AdjustThrottle()
    {
        throttleCurrent += throttleInput * Time.deltaTime * acceleration;// slowly adjusts accelleration
        throttleCurrent = Mathf.Clamp(throttleCurrent, throttleMin, throttleMax);//clamps throttle to min and max
    }

    void MovementUpdate()
    {
        if (Mathf.Abs(rb.velocity.magnitude - throttleCurrent) > 0.01f)
        {
            if (rb.velocity.magnitude < throttleMax)
            {
                rb.AddRelativeForce(Vector3.forward * (throttleInput * thrust));
                
            }

            if (rb.velocity.magnitude > throttleMax)
            {
                rb.drag += 0.1f;
            }
            else
            {
                rb.drag = 0;
            }
        }
    }

}
