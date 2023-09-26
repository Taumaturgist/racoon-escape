using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private List<Transform> _childsTransforms = new();
    private Transform _parentTransform;
    private float _force = 10f;

    private void Start()
    {
        _parentTransform = transform.GetChild(0);

        for (var i = 0; i < _parentTransform.childCount; i++)
        {
            _childsTransforms.Add(_parentTransform.GetChild(i));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        foreach (var childTransform in _childsTransforms)
        {
            var rb = childTransform.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(Random.onUnitSphere * _force);
        }
    }
}