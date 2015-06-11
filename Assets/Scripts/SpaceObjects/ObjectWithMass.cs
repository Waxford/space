using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ObjectWithMass : MonoBehaviour {

    public new Rigidbody rigidbody;
    [SerializeField]
    private Vector3 initialVelocity;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.velocity = initialVelocity;
    }
}
