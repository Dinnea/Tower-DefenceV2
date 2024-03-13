using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionSameLayer : MonoBehaviour
{
    Collider _collider;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(3, 3);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag(gameObject.tag))
    //    {
    //        Physics.IgnoreCollision(_collider, other);
    //    }
    //}
}
