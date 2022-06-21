using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    [SerializeField] Image Panel;
    [SerializeField] Text ResultText;
    [SerializeField] GameObject RestartButton;

    public static Results Instance;

    string endTime;

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

    public void EndGame()
    {
        Spawner.Instance.StopGame();
        MapMovement.Instance.StopMovement();
        endTime = Timer.Instance.StopTimer();

        StartCoroutine(FadeInPanel());
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator FadeInPanel()
    {
        Color alpha = Panel.color;
        Panel.enabled = true;

        while (alpha.a < 0.4f)
        {
            alpha.a += 0.01f;
            Panel.color = alpha;

            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);
        ResultText.text = "Survived " + endTime;

        yield return new WaitForSeconds(1f);
        RestartButton.SetActive(true);
    }
}
