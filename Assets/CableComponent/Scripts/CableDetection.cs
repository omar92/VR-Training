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

    

    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag == "EndBase" && other.gameObject.GetComponent<MeshRenderer>().material.color == material.color)
        {
            Correct = true;
            GetComponent<Rigidbody>().isKinematic = false;
            transform.position = other.transform.position;
        }
        else
        {

            Correct = false;
            
        }
    }
    private void OnCollisionExit(Collision other)
    {
        Correct = false;
    }
}
