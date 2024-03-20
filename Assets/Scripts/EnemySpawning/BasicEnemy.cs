using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(IgnoreCollisionSameLayer))]
public class BasicEnemy : MonoBehaviour, IEnemy
{
    NavMeshAgent _agent;
    Vector3[] _navPoints;
    int _pointsReached = 0;

    float _health;
    float _speed;
    float _money;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject[] navPoints = GameObject.FindGameObjectsWithTag("NavPoint");
        _navPoints = new Vector3[navPoints.Length];
        foreach(GameObject navPoint in navPoints)
        {
            NavPoint point = navPoint.GetComponent<NavPoint>();
            _navPoints[point.index] = point.GetNavLocation();
            
        }
        
    }
    private void Update()
    {
        Move();
        Die();
    }
    public void Move()
    {
        if(_pointsReached < _navPoints.Length)
        {
            _agent.destination = _navPoints[_pointsReached];
            if (transform.position.x == _navPoints[_pointsReached].x && transform.position.z == _navPoints[_pointsReached].z)
            {
                _pointsReached++;
            }
        }
    }

    public void Die()
    {
        if (_pointsReached == _navPoints.Length)
        {
            Destroy(gameObject);
        }
    }

    public void SetHealth(float pHealth)
    {
        _health = pHealth;
    }

    public void SetSpeed(float pSpeed)
    {
        _speed = pSpeed;
    }

    public void SetMoney(float pMoney)
    {
        _money = pMoney;
    }
}
