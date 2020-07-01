using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour
{
    public Event scoreEvent;
    private Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ScoreCollider"))
        {
            scoreEvent.Raise();
            DestroyThis();
        }
    }

    public void Throw(Vector3 force)
    {
        transform.SetParent(null);
        m_rigidbody.isKinematic = false;
        m_rigidbody.AddRelativeForce(force);
        Invoke("DestroyThis",3f);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
