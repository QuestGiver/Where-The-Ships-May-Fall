using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointValueCapsule : MonoBehaviour
{
    [SerializeField]
    float valueMax, valueCurrent  = 100f;
    [SerializeField]
    float valueMin, subtractionMultiplier, additionMultiplier = 0;

    public float ValueCurrent { get => valueCurrent; set => valueCurrent = value; }

    public void AddValue(float value)
    {
        ValueCurrent += value * additionMultiplier;
        if (ValueCurrent > valueMax)
        {
            ValueCurrent = valueMax;
        }
    }

    public void SubtractValue(float value)
    {
        ValueCurrent -= value * subtractionMultiplier;
        if (ValueCurrent < valueMin)
        {
            ValueCurrent = valueMin;
        }
    }


}
