using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour, IEnemy
{
    NavMeshAgent _agent;
    Vector3[] _navPoints;
    int _pointsReached = 0;
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
        if (_pointsReached > _navPoints.Length)
        {
            Destroy(gameObject);
        }
    }
}
