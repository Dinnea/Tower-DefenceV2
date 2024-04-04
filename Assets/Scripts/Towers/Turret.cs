using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] SingleTargetTower _owner;
    [SerializeField] ParticleSystem _vfx;

    void Start()
    {
        _owner = GetComponentInParent<SingleTargetTower>();
        _vfx = GetComponentInChildren<ParticleSystem>();
        _owner.onAction += playVFX;
    }

    private void Update()
    {
        rotateToTarget();
    }
    /// <summary>
    /// Always looks at parent SingleTargetTower's target.
    /// </summary>
    void rotateToTarget()
    {
        Transform temp = _owner.GetTargetLocation();
        if (temp != null)
        {
            transform.LookAt(temp);
        }
    }
    void playVFX()
    {
        _vfx.Play();
    }

}
