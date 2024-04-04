using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    [SerializeField]Transform _navLocation;
    public int index;
    private void Awake()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        _navLocation = renderer.transform;
    }

    public Vector3 GetNavLocation()
    {
        return _navLocation.position;
    }
}
