using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPin : MonoBehaviour
{
    public bool occupied;
    public Vector3 Position
    {
        get
        {
            return transform.position;
        } 
    }

}
