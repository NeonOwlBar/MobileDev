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

    [Header("Screen Bounds")]
    public static Rect safeArea;
    public float minX;
    public float maxX;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(spawnMin, spawnMax);

        // stores safe area data in new rect
        safeArea = Screen.safeArea;
        Debug.Log("Safe Area X = " + safeArea.x);
        Debug.Log("Safe Area width  = " + safeArea.width);

        // find where screen edge is in world space
        minX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.x, 0, 0)).x;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.width, 0, 0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        spawnMax = spawnMax <= spawnMin ? spawnMax : spawnMax - 0.0001f;

        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            // Generates position with random x co-ordinate for spawn point
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 7.0f, 0.0f);
            // instantiate enemy prefab at position defined above as a child of this empty game object
            GameObject newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity, gameObject.transform);
            // add the enemy behaviour class of the prefab to the list in world states
            WorldStates.enemies.Add(newEnemy.GetComponent<EnemyBehaviour>());
            // reset timer
            timer = 0;
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
    }
}
