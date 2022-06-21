using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            Destroy(other.gameObject);
            Health.Instance.LoseHealth();
        }
    }
}
