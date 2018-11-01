using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearBox : MonoBehaviour {
    public List<GearStack> gears;

    List<GearPin> pins = new List<GearPin>();

    public GearPin pinPrefab;

#if UNITY_EDITOR
    public bool DrawGizmos;
#endif

    public List<Transform> pinsPivots;
    List<GearRotation> gearRotations = new List<GearRotation>();

    private void Awake()
    {
        foreach(Transform pinPivot in pinsPivots)
        {
            GearPin newPin = Instantiate(pinPrefab);
            newPin.transform.position = pinPivot.position;
            pins.Add(newPin);
        }

        foreach (GearStack gear in gears)
        {
            gearRotations.Add(gear.GetComponent<GearRotation>());
            gear.pins = pins;
        }
    }

    public void SpurtGears()
    {
        foreach (GearStack gear in gears)
        {
            gear.rb.useGravity = true;
            gear.rb.AddForce(Random.insideUnitCircle * 5, ForceMode.Impulse);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.red;
            foreach (Transform pin in pinsPivots)
            {
                Gizmos.DrawSphere(pin.position, 0.05f);
            }
        }
    }
#endif
}
