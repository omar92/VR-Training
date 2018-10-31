using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Events;
using Raskulls.Variables;
//using 
public class CoverPuzzle : MonoBehaviour {

    public GameEvent Level1FinishedEvent;

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
        Level1FinishedEvent.Raise();
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
