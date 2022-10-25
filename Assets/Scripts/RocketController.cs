using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    Vector3 startingPoint, apogeeTransition, apogeeOriginal, endPoint;//Apogee is intended to interpolate from it's original position to endPoint.
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float ApogeeTransitionScaler = 2f;
    [SerializeField]
    float time = 0f;
    [SerializeField]
    ProjectileSpawner projectileSpawner;
    [SerializeField]
    bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interpolate();
        if (transform.position == endPoint)
        {
            SetZero();
            gameObject.SetActive(false);
            projectileSpawner.RocketPool.Enqueue(this);
        }
    }

    void Interpolate()
    {
        time += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(startingPoint, apogeeTransition, time);
        apogeeTransition = Vector3.Lerp(apogeeOriginal, endPoint, time / 2);
    }

    public void SetSpawner(ProjectileSpawner spawner)
    {
        projectileSpawner = spawner;
    }

    public void SetRocketController(Vector3 _startPoint, Vector3 _endPoint, Vector3 _apogeeTransition, Vector3 _apogeeOriginal, float _speed)
    {
        startingPoint = _startPoint;
        endPoint = _endPoint;
        apogeeTransition = _apogeeTransition;
        apogeeOriginal = _apogeeOriginal;
        speed = _speed;
    }

    void SetZero()
    {
        startingPoint = Vector3.zero;
        endPoint = Vector3.zero;
        apogeeTransition = Vector3.zero;
        apogeeOriginal = Vector3.zero;
        speed = 0f;
        time = 0f;
        isFired = false;
    }
}
