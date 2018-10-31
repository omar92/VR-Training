using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSystem : MonoBehaviour {
    public List<CableDetection> EndBase;

    private void Update()
    {
        if (check())
            print("Cool");
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
