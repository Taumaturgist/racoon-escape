using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    private readonly List<Transform> _childsTransforms = new();
    private readonly float _force = 5f;
    private bool _firstCollition = true;

    private void Start()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            _childsTransforms.Add(transform.GetChild(i));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && _firstCollition)
        {
            _firstCollition = false;
            StartCoroutine("DestroyObject");
        }
    }

    private IEnumerator DestroyObject()
    {
        foreach (var childTransform in _childsTransforms)
        {
            var rb = childTransform.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(Random.onUnitSphere * _force);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}