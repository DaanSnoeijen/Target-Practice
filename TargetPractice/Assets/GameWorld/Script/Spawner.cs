using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject Target;

    public static Spawner Instance;

    Coroutine routine1;
    Coroutine routine2;

    float spawnTime;
    float betweenTime;
    int spawnAmount;

    System.Random random = new System.Random();

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

    public void StartGame()
    {
        spawnTime = 5f;
        betweenTime = 1.5f;
        spawnAmount = 1;

        routine1 = StartCoroutine(IStartGame());
    }

    public void StopGame()
    {
        StopCoroutine(routine1);
        StopCoroutine(routine2);
    }

    IEnumerator IStartGame()
    {
        yield return new WaitForSeconds(3f);
        routine2 = StartCoroutine(SpawnCycle());

        int wave = 0;

        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (spawnTime > 0.3f) spawnTime -= 0.3f;
            if (betweenTime > 0.1f) betweenTime -= 0.1f;

            if (wave == 10)
            {
                spawnTime += 2.8f;
                betweenTime += 0.95f;
                spawnAmount++;

                wave = 0;
            }

            wave++;
        }
    }

    IEnumerator SpawnCycle()
    {
        while (true)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject target = Instantiate(Target, transform);
                target.GetComponent<Rigidbody>().AddForce(new Vector3(random.Next(-20, 21)/10f, 9f, 0f), ForceMode.VelocityChange);

                yield return new WaitForSeconds(betweenTime);
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
