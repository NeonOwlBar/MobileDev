using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static float parallaxGroupMulti = 1.0f;
    public float parallaxLayerMulti;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = transform.position.y - 1.0f * parallaxLayerMulti * parallaxGroupMulti * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (transform.position.y < -length)
        {
            transform.position = new Vector3(transform.position.x, length * 0.98f, transform.position.z);
        }
    }

    public static void IncreaseSpeedMulti(float delta)
    {
        parallaxGroupMulti += delta;
    }
}
