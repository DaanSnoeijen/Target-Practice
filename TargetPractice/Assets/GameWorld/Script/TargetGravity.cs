using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGravity : MonoBehaviour
{
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        while (true)
        {
            rigidbody.AddForce(0f, -0.2f, 0f, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
