using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyManager : MonoBehaviour
{
    public int _spawnTimer = 60;
    public float distance = 15f;
    public GameObject enemyPrefab;

    private int t = 0;

    private void FixedUpdate()
    {
        t++;
        if (t >= _spawnTimer)
        {
            SpawnEnemy();
            t = 0;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomDirectionWithUnitLength = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * Vector2.right * distance;
        Instantiate(enemyPrefab, randomDirectionWithUnitLength, Quaternion.identity);
    }

}
