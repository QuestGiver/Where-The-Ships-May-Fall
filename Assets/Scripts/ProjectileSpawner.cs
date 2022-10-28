using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject RocketObjectSource;
    [SerializeField]
    public Queue<ProjectileMovement> RocketPool = new Queue<ProjectileMovement>();
    [SerializeField]
    int poolSize = 10;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    Transform Apogee;
    [SerializeField]
    float randomMin = -10f;
    [SerializeField]
    float randomMax = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (RocketPool.Count == 0)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject RocketObject = Instantiate(RocketObjectSource, transform.position, Quaternion.identity);
                RocketObject.SetActive(false);
                RocketObject.GetComponent<ProjectileMovement>().SetSpawner(this);
                RocketPool.Enqueue(RocketObject.GetComponent<ProjectileMovement>());
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Apogee.position, 0.5f);
    }
#endif
    public void GetAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            processAttackInput();
        }
    }

    void processAttackInput()
    {
        ProjectileMovement rocket = RocketPool.Dequeue();
        rocket.gameObject.SetActive(true);
        Vector3 random = new Vector3(Random.Range(randomMin, randomMax), Random.Range(randomMin, randomMax), 0);
        rocket.SetRocketController(transform.position, transform.position + transform.forward * 100f, Apogee.position + random, Apogee.position + random, speed);
    }

}
