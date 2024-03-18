using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class HealthBoost : Character
//{
//    private Rigidbody2D rb;

//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        rb.velocity = -Vector2.up * speed;
//        //transform.Translate(0, -(speed * Time.deltaTime), 0);
//        if (rb.position.y < -7.0f || health <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            health -= 1;
//        }
//    }
//}
