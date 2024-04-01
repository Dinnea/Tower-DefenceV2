using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] LaserTower _owner;
    [SerializeField] ParticleSystem _vfx;

    void Start()
    {
        _owner = GetComponentInParent<LaserTower>();
        _vfx = GetComponentInChildren<ParticleSystem>();
        _owner.onAction += playVFX;
    }

    private void Update()
    {
        rotate();
    }
    void rotate()
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

    //IEnumerator rotateCD
}
