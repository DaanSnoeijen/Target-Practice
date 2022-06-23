using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour, I_SmartwallInteractable
{
    [SerializeField] ParticleSystem Effect;

    public void Hit(Vector3 pos) => StartCoroutine(Action());

    //private void OnMouseDown() => StartCoroutine(Action());

    IEnumerator Action()
    {
        GameObject tempObject = new GameObject();
        tempObject.transform.position = transform.position;

        Instantiate(Effect, tempObject.transform);
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<SphereCollider>());

        yield return new WaitForSeconds(1f);
        Destroy(tempObject);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
