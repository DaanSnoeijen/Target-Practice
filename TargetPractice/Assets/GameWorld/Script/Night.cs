using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Night : MonoBehaviour
{
    [SerializeField] Light DirectionalLightTop;
    [SerializeField] Light DirectionalLightBottom;

    [SerializeField] Image NightFilter;

    [SerializeField] ParticleSystem Clouds;
    [SerializeField] ParticleSystem Stars;

    public static Night Instance;

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

    public void StartNight() { StartCoroutine(IStartNight()); }

    IEnumerator IStartNight()
    {
        Color NightColor = NightFilter.color;
        float CloudMoveSpeed = 0;

        for (int i = 0; i < 100; i++)
        {
            CloudMoveSpeed += 0.1f;
            Clouds.transform.position += transform.up * CloudMoveSpeed;
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(Clouds);
        Stars.Play();

        for (int i = 0; i < 100; i++)
        {
            NightColor.a += 0.005f;

            if (DirectionalLightBottom.intensity > 0.02f) DirectionalLightTop.intensity -= 0.01f;
            DirectionalLightBottom.intensity -= 0.01f;
            NightFilter.color = NightColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
