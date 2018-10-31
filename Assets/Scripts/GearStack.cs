using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GearStack : MonoBehaviour
{
    public List<GearPin> pins;
    public Raskulls.Variables.FloatVariable snappingDis;

    public Rigidbody rb;

    public float snapTime = 0.1f;

    Vector3 currentVelocity;
    
    public bool gearSnapping;

    public bool gearHeld;

    Vector3 pinPos;

    public GearRotation gearRotation;

    private void Update()
    {
        if (gearHeld)
        {
            rb.useGravity = false;
            gearRotation.enabled = false;
            gearSnapping = StackGear();
        }

        else if (gearSnapping)
        {
            rb.useGravity = false;
            gearRotation.enabled = true;
            transform.position = Vector3.SmoothDamp(transform.position, pinPos, ref currentVelocity, snapTime);
            if ((transform.position - pinPos).magnitude < 0.01f)
            {
                gearSnapping = false;
                transform.position = pinPos;
            }
        }
    }

    bool StackGear()
    {
        GearPin closestPin = ClosestPin();
        if (closestPin != null)
        {
            pinPos = closestPin.Position;
            print(pinPos);
            transform.forward = closestPin.transform.up;
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

            if (newDis < closestDis)
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
