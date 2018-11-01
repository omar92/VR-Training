using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurtObjects : MonoBehaviour
{
    public List<SpurtingObject> spurters;

    public Transform playerPos;

    public bool secondPuzzleGoing = true;
    public bool thirdPuzzleGoing = true;

    public void StartSpurting()
    {
        StartCoroutine(SpurtingCoroutine());
    }

    public void SecondPuzzleFinished()
    {
        secondPuzzleGoing = false;
    }

    public void ThirdPuzzleFinished()
    {
        thirdPuzzleGoing = false;
    }

    IEnumerator SpurtingCoroutine()
    {
        while (secondPuzzleGoing || thirdPuzzleGoing)
        {
            yield return new WaitForSeconds(Random.value * 3);
            SpurtingObject newSpurter =
                Instantiate(spurters[Random.Range(0, spurters.Count)], transform);

            newSpurter.rb.AddForce(
                (playerPos.position - newSpurter.transform.position).normalized *
                (Random.value * 10 + 5) +
                Random.insideUnitSphere * Random.value * 5,
                ForceMode.Impulse
                );
        }
    }
}
