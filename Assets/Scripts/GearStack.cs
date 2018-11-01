using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GearStack : MonoBehaviour
{
    [HideInInspector]
    public List<GearPin> pins;

    [SerializeField]
    Raskulls.Variables.FloatVariable snappingDis;

    public Rigidbody rb;

    [SerializeField]
    MeshCollider mc;

    [SerializeField]
    float snapTime = 0.1f;

    Vector3 currentVelocity;

    bool gearSnapping;

    bool gearFixed;

    public bool gearHeld;

    GearPin gearPin;

    public GearRotation gearRotation;

    public void IsGearHeld(bool value)
    {
        gearHeld = value;
    }

    private void Update()
    {
        if (gearHeld)
        {
            rb.useGravity = false;
            gearRotation.enabled = false;
            if (gearPin != null)
            {
                gearPin.occupied = false;
                gearPin = null;
            }
            gearSnapping = StackGear();
            rb.isKinematic = true;
            mc.isTrigger = true;
        }

        else if (gearSnapping)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
            rb.useGravity = false;
            gearRotation.enabled = true;
            transform.position = Vector3.SmoothDamp(transform.position, gearPin.Position,
                ref currentVelocity, snapTime);
            if ((transform.position - gearPin.Position).magnitude < 0.01f)
            {
                gearSnapping = false;
                transform.position = gearPin.Position;
            }
            mc.isTrigger = true;
        }
        else if(!gearFixed)
            mc.isTrigger = false;
    }

    bool StackGear()
    {
        GearPin closestPin = ClosestPin();
        gearFixed = false;
        if (closestPin != null)
        {
            gearPin = closestPin;
            transform.forward = closestPin.transform.up;
            gearFixed = true;
            return true;
        }
        return false;
    }

    GearPin ClosestPin()
    {
        float closestDis = (pins[0].Position - transform.position).magnitude;
        GearPin closestPin = pins[0];
        for (int i = 1; i < pins.Count; i++)
        {
            float newDis = (pins[i].Position - transform.position).magnitude;

            if (!pins[i].occupied && newDis < closestDis)
            {
                closestDis = newDis;
                closestPin = pins[i];
            }
        }
        if (closestDis < snappingDis.Value)
            return closestPin;
        return null;
    }
}
