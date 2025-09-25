using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public float spawnInterval = 3f;
    public float spawnRange = 5f;

    public float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            //x,z 는 랜덤 / y는 고정
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-spawnRange, spawnRange),
                transform.position.y,
                transform.position.z + Random.Range(-spawnRange, spawnRange)
                );

            Instantiate(enemyPrefabs, spawnPos, Quaternion.identity);
            timer = 0f;
        }

        /*void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange * 2, 0.1f, spawnRange * 2));
        }*/
    }
}
