using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
[RequireComponent(typeof(DamageDelivery))]
public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    ProjectileMovement projectileMovement;
    [SerializeField]
    DamageDelivery damageDelivery;

    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        projectileMovement.OnInterpolation.AddListener(CollisionCheck);
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = transform.position;
    }

    public void CollisionCheck()
    {
        if (Physics.Raycast(lastPosition, transform.position - lastPosition, out RaycastHit hit, Vector3.Distance(lastPosition, transform.position)))
        {
            if (hit.collider.gameObject.GetComponent<HitPoints>())
            {
                damageDelivery.DeliverDamage(hit.collider);
            }
        }
    }
    
}
