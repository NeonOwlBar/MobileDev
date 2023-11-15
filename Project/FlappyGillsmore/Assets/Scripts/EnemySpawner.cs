using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    float timer;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            // Generates position with random x co-ordinate for spawn point
            Vector3 spawnPosition = new Vector3(Random.Range(-1.75f, 1.75f), 5.0f, 0.0f);
            // 4th parameter makes any instances a child of the "Enemies" empty game object
            Instantiate(prefab, spawnPosition, Quaternion.identity, this.gameObject.transform);

            timer = 0;
        }
    }
}
