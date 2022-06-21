using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Health : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;

    [SerializeField] RawImage Heart1;
    [SerializeField] RawImage Heart2;
    [SerializeField] RawImage Heart3;

    [SerializeField] Texture HeartLess;

    public static Health Instance;

    Vignette vignette;
    Coroutine routine;

    int health = 3;

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

    private void Start()
    {
        volume.profile.TryGetSettings(out vignette);
    }

    public void SetHearts()
    {
        Color alhpa = new Color(1f, 1f, 1f, 1f);

        Heart1.color = alhpa;
        Heart2.color = alhpa;
        Heart3.color = alhpa;
    }

    public void LoseHealth() 
    {
        if (routine == null) health--;

        if (health == 2) Heart3.texture = HeartLess;
        else if (health == 1) Heart2.texture = HeartLess;
        else if (health == 0)
        {
            Heart1.texture = HeartLess;
            Results.Instance.EndGame();
        }

        if (routine == null && health > 0) routine = StartCoroutine(ILoseHealth()); 
    }

    IEnumerator ILoseHealth()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                vignette.color.value = new Color(0.04f * j, 0f, 0f);
                vignette.intensity.value += 0.004f;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(0.2f);

            for (int j = 25; j > 0; j--)
            {
                vignette.color.value = new Color(0.04f * j, 0f, 0f);
                vignette.intensity.value -= 0.004f;
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(0.2f);
        }

        routine = null;
    }
}
