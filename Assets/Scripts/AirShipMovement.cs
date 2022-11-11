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
    float throttleInput, throttleMin, throttleMax, throttleCurrent, throttleStage;
    [SerializeField]
    float elevationInput, elevationMin, elevationMax, elevationCurrent;
    [SerializeField]
    float rotationTorque;
    [SerializeField]
    float rotationInput, rotationSpeedMin, rotationSpeedMax, rotationSpeedCurrent;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    DelineateInput delineatedInput;
    // Start is called before the first frame update
    void Start()
    {
        delineatedInput = GetComponent<DelineateInput>();
        delineatedInput.OnInput += GetThrottleInput;
        delineatedInput.OnInput += GetElevationInput;
        delineatedInput.OnInput += GetRotationInput;
        Debug.Log("delineatedInput: " + delineatedInput);
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

    public void GetThrottleInput()
    {
        throttleInput = delineatedInput.ReturnThrottleInput();
        throttleStage = Mathf.Clamp(throttleStage += throttleInput,-1,4);
       
    }

    public void GetElevationInput()
    {
        elevationInput = delineatedInput.ReturnElevationInput();
    }

    public void GetRotationInput()
    {
        rotationInput = delineatedInput.ReturnRotationInput();
    }

    void ProcessThrottleInput()
    {
        throttleCurrent += (throttleStage/4) * Time.deltaTime * acceleration;// slowly adjusts accelleration
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
        Vector2 horizontalProperties = new Vector2(rb.velocity.x, rb.velocity.z);

        if (Mathf.Abs(horizontalProperties.magnitude - throttleCurrent) > 0f)
        {
            if (horizontalProperties.magnitude < throttleMax)
            {
                if ((throttleStage / 4) != 0)// if the throttle is not at zero
                {
                    //if trying to move in the opposite direction of current velocity, increase thrust
                    if (Vector2.Dot((throttleStage / 4) * transform.forward, new Vector3(horizontalProperties.x, 0, horizontalProperties.y)) < 0)//this denotes that some of the desired velocity is , at least,  somewhat in the opposite direction to the current horizontal velocity
                    {
                        rb.AddRelativeForce(Vector3.forward * ((throttleStage / 4) * thrust * breakThrustModifier));// gets the throttle and multiplies the thrust by it to produce a positive or negative, also gives increased thrust becasue above condition is true
                    }
                    else
                    {
                        rb.AddRelativeForce(Vector3.forward * ((throttleStage / 4) * thrust));// normal thrust
                    }
                }
            }

            if (horizontalProperties.magnitude > (throttleMax * Mathf.Abs(throttleStage / 4)))// limmits speed based on throttle and throttle max
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
