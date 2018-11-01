using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ParticleSystem particleSystem;
    public MeshRenderer face;
    public Material angry;
    public Material happy;

    public bool secondPuzzleGoing = true;
    public bool thirdPuzzleGoing = true;

    public void StartAngryFace()
    {
        face.material = angry;
        particleSystem.Play();
    }

    public void SecondPuzzleFinished()
    {
        secondPuzzleGoing = false;
    }

    public void ThirdPuzzleFinished()
    {
        thirdPuzzleGoing = false;
    }

    public void DoneFixing()
    {
       if(!secondPuzzleGoing && !thirdPuzzleGoing)
        {
            face.material = happy;
            particleSystem.Stop();
        }
    }
}
