using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoints : MonoBehaviour
{
    [SerializeField]
    PointValueCapsule damagePoints;
    [SerializeField]
    int targetLayer = 6;
    [SerializeField]
    bool expended = false;

    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.layer == targetLayer) && !expended)
        {
            collider.gameObject.GetComponent<HitPoints>().hitPoints.SubtractValue(damagePoints.ValueCurrent);
            expended = true;
        }
    }
}
