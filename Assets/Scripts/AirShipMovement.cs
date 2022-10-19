using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AirShipMovement : MonoBehaviour
{
    [SerializeField]
    float dragMin;
    [SerializeField]
    float thrust = 10f;//Also a limmit on the abstract concecpt of "engine power availible"
    [SerializeField]
    float breakThrustModifier = 1f;
    [SerializeField]
    float acceleration = 10f;//How fast the ship can accelerate
    [SerializeField]
    float throttleInput, throttleMin, throttleMax, throttleCurrent;
    [SerializeField]
    float elevationInput, elevationMin, elevationMax, elevationCurrent;
    [SerializeField]
    float rotationTorque;
    [SerializeField]
    float rotationInput, rotationSpeedMin, rotationSpeedMax, rotationSpeedCurrent;
    [SerializeField]
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        ProcessThrottleInput();
        ProcessElevationInput();
        ProcessRotationInput();
        MovementUpdate();
    }
    
    void MovementUpdate()
    {
        ThrottleUpdate();
        ElevationUpdate();
        RotationUpdate();
    }

    public void GetThrottleInput(InputAction.CallbackContext context)
    {
        throttleInput = context.ReadValue<float>();
    }

    public void getElevationInput(InputAction.CallbackContext context)
    {
        elevationInput = context.ReadValue<float>();
    }

    public void getRotationInput(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
    }

    void ProcessThrottleInput()
    {
        throttleCurrent += throttleInput * Time.deltaTime * acceleration;// slowly adjusts accelleration
        throttleCurrent = Mathf.Clamp(throttleCurrent, throttleMin, throttleMax);//clamps throttle to min and max
    }

    void ProcessRotationInput()
    {
        rotationSpeedCurrent += rotationInput * Time.deltaTime * rotationTorque;// slowly adjusts rotation accelleration
        rotationSpeedCurrent = Mathf.Clamp(rotationSpeedCurrent, rotationSpeedMin, rotationSpeedMax);// clamps rotation to min and max
    }

    void ProcessElevationInput()
    {
        elevationCurrent += elevationInput * Time.deltaTime * acceleration; // slowly adjusts elevation accelleration
        elevationCurrent = Mathf.Clamp(elevationCurrent, elevationMin, elevationMax); // clamps elevation to min and max
    }



    private void ThrottleUpdate()
    {
        Vector2 horizontalComponents = new Vector2(rb.velocity.x, rb.velocity.z);

        if (Mathf.Abs(horizontalComponents.magnitude - throttleCurrent) > 0f)
        {
            if (horizontalComponents.magnitude < throttleMax)
            {
                //if trying to move in the opposite direction of current velocity, increase thrust
               if (Vector2.Dot(throttleInput * transform.forward, new Vector3(horizontalComponents.x, 0, horizontalComponents.y)) < 0)//this denotes that some of the desired velocity is , at least,  somewhat in the opposite direction to the current horizontal velocity
                {
                    rb.AddRelativeForce(transform.forward * (throttleInput * thrust * breakThrustModifier));
                }
                else
                {
                    rb.AddRelativeForce(transform.forward * (throttleInput * thrust));
                }

            }

            if (horizontalComponents.magnitude > throttleMax)
            {
                rb.drag += 0.1f;
            }
            else
            {
                rb.drag = dragMin;
            }
        }
    }
    
    private void ElevationUpdate()
    {
        if (Mathf.Abs(rb.velocity.y - elevationCurrent) > 0.01f)
        {

            if (elevationInput == 0 && (rb.velocity.y > 0))
            {
                rb.drag += 0.1f;
            }
            else
            {
                rb.drag = dragMin;
            }

            if (rb.velocity.y < elevationMax)
            {
                rb.AddRelativeForce(Vector3.up * (elevationInput * thrust));
            }

            if (rb.velocity.y > elevationMax)
            {
                if (elevationInput != 0)
                rb.drag += 0.1f;
            }
            else
            {
                rb.drag = dragMin;
            }
        }
    }

    private void RotationUpdate()
    {

        if (Mathf.Abs(rb.angularVelocity.magnitude - rotationSpeedCurrent) > 0.01f)
        {

            if (rotationInput == 0 && (rb.angularVelocity.magnitude > 0))
            {
                rb.angularDrag += 0.1f;
            }
            else
            {
                rb.angularDrag = 0;
            }

            if (rb.angularVelocity.magnitude < rotationSpeedMax)
            {
                rb.AddRelativeTorque(Vector3.up * (rotationInput * rotationTorque));
            }

            if (rb.angularVelocity.magnitude > rotationSpeedMax)
            {
                rb.angularDrag += 0.1f;
            }
            else
            {
                if (rotationInput != 0)
                    rb.angularDrag = 0;
            }

        }
    }



}
