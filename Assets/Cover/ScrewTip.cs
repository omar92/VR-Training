using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewTip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var screw = other.GetComponent<ScrewScript>();
        if (screw)
        {
            screw.ScrewRemove();
        }

    }
}
