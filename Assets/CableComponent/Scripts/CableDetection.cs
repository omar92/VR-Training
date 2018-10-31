using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableDetection : MonoBehaviour {
    public CableSystem cableSystem;
    public bool Correct;
    public Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "EndBase" && other.gameObject.GetComponent<MeshRenderer>().material.color == material.color) 
            Correct = true;
        else
            Correct = false;
    }
    private void OnTriggerExit(Collider other)
    {
        Correct = false;
    }
}
