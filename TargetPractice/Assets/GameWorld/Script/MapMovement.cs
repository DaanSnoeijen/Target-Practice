using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public static MapMovement Instance;

    Coroutine routine;

    float moveSpeed = -0.06f;

    private void Awake()
    {
        //Create a singleton pattern that doesn't get destroyed on load
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMovement()
    {
        routine = StartCoroutine(Movement());
    }

    public void StopMovement()
    {
        StopCoroutine(routine);
    }

    IEnumerator Movement()
    {
        while (true)
        {
            transform.position += transform.forward * moveSpeed;

            yield return new WaitForSeconds(0.02f);
        }
    }
}
