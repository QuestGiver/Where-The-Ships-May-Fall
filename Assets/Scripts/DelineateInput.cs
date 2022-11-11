using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DelineateInput : MonoBehaviour
{
    public abstract float ReturnThrottleInput();
    public abstract float ReturnElevationInput();
    public abstract float ReturnRotationInput();
    public delegate void ReturnInputDelegate();
    public ReturnInputDelegate OnInput;
}
