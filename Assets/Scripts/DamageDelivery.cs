using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PointValueCapsule))]
public class DamageDelivery : MonoBehaviour
{
    [SerializeField]
    PointValueCapsule damagePoints;
    [SerializeField]
    int targetLayer = 6;
    [SerializeField]
    bool expended = false;


    public void DeliverDamage(Collider collider)
    {
        if ((collider.gameObject.layer == targetLayer) && !expended)
        {
            collider.gameObject.GetComponent<HitPoints>().hitPoints.SubtractValue(damagePoints.ValueCurrent);
            expended = true;
        }
    }
}
