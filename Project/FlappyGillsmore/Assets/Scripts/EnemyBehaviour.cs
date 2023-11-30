using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : Character
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetHealthMax(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        rb.velocity = -Vector2.up * speed;
        //transform.Translate(0, -(speed * Time.deltaTime), 0);
        if (rb.position.y < -6.0f)
            Destroy(gameObject);
    }
}
