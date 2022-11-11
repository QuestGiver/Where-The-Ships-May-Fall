using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReceiver : DelineateInput
{
    [SerializeField]
    private float throttleInput;
    [SerializeField]
    private float elevationInput;
    [SerializeField]
    private float rotationInput;

    public void GetThrottleInput(InputAction.CallbackContext context)
    {
        throttleInput = context.ReadValue<float>();
        OnInput.Invoke();
        
    }
    public void GetElevationInput(InputAction.CallbackContext context)
    {
        elevationInput = context.ReadValue<float>();
        OnInput.Invoke();
        
    }
    public void GetRotationInput(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
        OnInput.Invoke();
        
    }

    public override float ReturnThrottleInput()
    {
        return throttleInput;
    }

    public override float ReturnElevationInput()
    {
        return elevationInput;
    }

    public override float ReturnRotationInput()
    {
        return rotationInput;
    }

        
}
