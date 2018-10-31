using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using 
public class CoverPuzzle : MonoBehaviour {
    readonly bool[] IsScrowWelded = new bool[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void ScrewRemoved(int screwNUm)
    {
        IsScrowWelded[screwNUm-1] = false;

       if(CheckIsPuzzelFinished())
        {
            PuzzelFinishedAction();
        }
    }

    private void PuzzelFinishedAction()
    {
       
    }

    private bool CheckIsPuzzelFinished()
    {
        for (int i = 0; i < IsScrowWelded.Length; i++)
        {
            if (IsScrowWelded[i])
            {
                return false;
            }
        }
        return true;
    }
}
