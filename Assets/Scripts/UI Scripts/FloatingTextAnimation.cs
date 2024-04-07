using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextAnimation : MonoBehaviour
{
    [SerializeField] float _destroyAfter = 2f;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(0);
        Destroy(gameObject, _destroyAfter);
    }
}
