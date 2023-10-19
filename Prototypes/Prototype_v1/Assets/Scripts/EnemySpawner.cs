using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 3)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1.75f, 1.75f), 3.0f, 0.0f);
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            spawnTimer = 0;
        }
    }
}
