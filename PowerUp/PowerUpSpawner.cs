using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerUpPrefab;
    public float spawnInterval = 20f;

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Instantiate(powerUpPrefab, transform.position, transform.rotation);
        }
    }
}
