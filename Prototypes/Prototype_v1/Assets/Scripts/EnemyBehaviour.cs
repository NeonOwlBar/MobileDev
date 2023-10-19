using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -(speed * Time.deltaTime), 0);
        if (transform.position.y < -6.0f)
            Destroy(gameObject);
    }
}
