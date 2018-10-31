using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurtObjects : MonoBehaviour
{
    public List<SpurtingObject> spurters;

    public Transform playerPos;

    public bool Spurting = true;

    public void StartSpurting()
    {
        StartCoroutine(SpurtingCoroutine());
    }

    public void StopSpurting()
    {
        Spurting = false;
    }

    IEnumerator SpurtingCoroutine()
    {
        while (Spurting)
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
