using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurtingObject : MonoBehaviour {

    public Rigidbody rb;

    float lifeTime;

	// Use this for initialization
	void Start () {
        lifeTime = 10;
    }
	
	// Update is called once per frame
	void Update () {
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        else
            Destroy(gameObject);
	}
}
