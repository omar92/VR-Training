using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Events;
public class CableSystem : MonoBehaviour {
    public List<CableDetection> EndBase;
    public GameEvent Puzzle1WinEvent;

    
    private void Update()
    {
        if (check())
        {
            Puzzle1WinEvent.Raise();
            enabled = false;
        }
    }

    public bool check()
    {
        bool temp = true;
        for (int i = 0; i < EndBase.Count; i++)
        {
            temp = (temp && EndBase[i].Correct);
        }
        return temp;

    }
}
