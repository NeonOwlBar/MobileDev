using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Character
{
    // Update is called once per frame
    void Update()
    {
        EnemyMove();

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        transform.Translate(0, -(speed * Time.deltaTime), 0);
        if (transform.position.y < -6.0f)
            Destroy(gameObject);
    }
}
