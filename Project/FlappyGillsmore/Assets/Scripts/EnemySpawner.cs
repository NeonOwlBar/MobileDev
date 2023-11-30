using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    public float spawnMin = 0.8f;
    public float spawnMax = 1.5f;
    float timer;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax);
    }

    // Update is called once per frame
    void Update()
    {

        spawnMax = spawnMax <= spawnMin ? spawnMax : spawnMax - 0.0001f;
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            // Generates position with random x co-ordinate for spawn point
            Vector3 spawnPosition = new Vector3(Random.Range(-1.75f, 1.75f), 5.0f, 0.0f);
            // instantiate enemy prefab at position defined above as a child of this empty game object
            Instantiate(prefab, spawnPosition, Quaternion.identity, this.gameObject.transform);
            // reset timer
            timer = 0;
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
    }
}
