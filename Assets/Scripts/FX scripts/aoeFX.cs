using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeFX : MonoBehaviour
{
    Tower _owner;
    MeshRenderer _mesh;
    [SerializeField] float _fadeSpeed = 0.5f;
    Color _originalColor;
    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _originalColor = _mesh.material.color;
        _owner = GetComponentInParent<Tower>();
    }
    private void Start()
    {
        setRadius(_owner.GetRange());
    }
        
    private void OnEnable()
    {
            _owner.onAction += triggerVFX;
    }
    private void OnDisable()
    {
        _owner.onAction -= triggerVFX;
    }
    private void setRadius(float radius)
    {
        transform.localScale = new Vector3(radius * 2, transform.localScale.y, radius * 2);
    }
    /// <summary>
    /// Ensures that the VFX mesh is enable and restarts the fadeOut coroutine.
    /// </summary>
    private void triggerVFX()
    {
        StopAllCoroutines();
        _mesh.material.color = _originalColor;
        _mesh.enabled = true;
        StartCoroutine(fadeOut());
    }

    private IEnumerator fadeOut()
    {
        while (_mesh.material.color.a > 0)
        {
            Color color = _mesh.material.color;
            float fadeAmount = color.a - (_fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _mesh.material.color = color;
            yield return null;
        }
        _mesh.material.color = _originalColor;
        _mesh.enabled = false;
    }
}
