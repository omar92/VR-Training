using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrewScript : MonoBehaviour {

    public int screwNum = 0;
    public float throwForce=5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ScrewRemove()
    {
        var cover = GetComponentInParent<CoverPuzzle>();
        if (cover)
        {
            cover.ScrewRemoved(screwNum);
        }

        GetComponent<Rigidbody>().AddForce(transform.up*throwForce); 
    }
}
