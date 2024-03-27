using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    [SerializeField]Transform _navLocation;
    public int index;
    private void Awake()
    {
        MeshRenderer temp = GetComponentInChildren<MeshRenderer>();
        _navLocation = temp.transform;
        //temp.enabled = false;
    }

    public Vector3 GetNavLocation()
    {
        return _navLocation.position;
    }
}
