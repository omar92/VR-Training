using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    float angularVelocity;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public Vector3 AngularVelocity
    {
        get
        {
            return rigidBody.angularVelocity;
        }
        set
        {
            rigidBody.angularVelocity = value;
        }
    }

    public float Diameter
    {
        get
        {
            return meshRenderer.bounds.size.x;
        }
    }

    private void Update()
    {
        if (angularVelocity > 0)
            rigidBody.angularVelocity = new Vector3(0, 0, angularVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Gear"))
        {
            GearRotation otherGear = other.GetComponent<GearRotation>();
            if (otherGear.AngularVelocity.z > 0)
            {
                AngularVelocity = -otherGear.AngularVelocity * (otherGear.Diameter / Diameter);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rigidBody.angularVelocity = Vector3.zero;
    }
}
