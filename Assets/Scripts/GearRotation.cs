using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearRotation : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    float angularVelocity;

    [SerializeField]
    UnityEvent gearMoved;

    GearRotation drivingMotor;

    public int drivingID = 1000;

    public bool load;

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
            if (Mathf.Abs(angularVelocity) > 0)
                return new Vector3(angularVelocity, 0, 0);
            else if (drivingMotor)
                return -drivingMotor.AngularVelocity * (drivingMotor.Diameter / Diameter);
            return Vector3.zero;
        }
        set
        {
            angularVelocity = value.x;
        }
    }

    public float Diameter
    {
        get
        {
            return meshRenderer.bounds.size.x;
        }
    }

    private void Awake()
    {
        rigidBody.angularVelocity = AngularVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (drivingID > 0 && other.tag.Equals("Gear"))
        {
            GearRotation otherGear = other.GetComponent<GearRotation>();
            if (otherGear.drivingID < drivingID)
            {
                drivingID = otherGear.drivingID + 1;
                drivingMotor = otherGear;
                rigidBody.angularVelocity = AngularVelocity;
                if (load)
                {
                    load = false;
                    StartCoroutine(GearStillMoving());
                }
            }
        }
    }

    IEnumerator GearStillMoving()
    {
        yield return new WaitForSeconds(1.5f);
        if(AngularVelocity.x > 0)
        {
            gearMoved.Invoke();
        }
        else
        {
            load = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (drivingID > 0 && other.tag.Equals("Gear"))
        {
            GearRotation otherGear = other.GetComponent<GearRotation>();
            if (otherGear.drivingID < drivingID)
            {
                drivingID = otherGear.drivingID + 1;
                drivingMotor = otherGear;
                rigidBody.angularVelocity = AngularVelocity;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GearRotation otherGear = other.GetComponent<GearRotation>();

        if (drivingID > 0 && drivingMotor && other.GetInstanceID() == drivingMotor.GetInstanceID())
        {
            drivingMotor = null;
            rigidBody.angularVelocity = AngularVelocity;
            drivingID = 1000;
        }
    }
}
