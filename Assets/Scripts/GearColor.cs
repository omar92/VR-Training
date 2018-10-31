using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearColor : MonoBehaviour
{

    [SerializeField]
    MeshRenderer meshRenderer;

    [SerializeField]
    Material movingMaterial;

    [SerializeField]
    Material stoppedMaterial;

    [SerializeField]
    GearRotation gearRotation;

    // Update is called once per frame
    void Update()
    {
        if (gearRotation.AngularVelocity.magnitude > 0)
        {
            meshRenderer.material = movingMaterial;
        }
        else
        {
            meshRenderer.material = stoppedMaterial;
        }
    }
}
