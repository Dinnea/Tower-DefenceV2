using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EventBus<Event>;

[RequireComponent(typeof(IgnoreCollisionSameLayer))]
public class BasicEnemy : Enemy
{
    [SerializeField] Vector3[] _navPoints;
    int _pointsReached = 0;
    

    // -------------- Abstract Enemy ----------------//
    protected override void OnAwake()
    {
        base.OnAwake();
        GameObject[] navPoints = GameObject.FindGameObjectsWithTag("NavPoint");
        _navPoints = new Vector3[navPoints.Length];
        foreach (GameObject navPoint in navPoints)
        {
            NavPoint point = navPoint.GetComponent<NavPoint>();
            _navPoints[point.index] = point.GetNavLocation();
        }

        //Move(_navPoints[_pointsReached]);
    }
    protected override void OnUpdate()
    {
        if(autoNavigate)navigate();
        
        base.OnUpdate();
    }

    private void navigate()
    {
        if (_pointsReached < _navPoints.Length)
        {
            if(Move(_navPoints[_pointsReached])) _pointsReached++;
        }
    }
}
