using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static float parallaxGroupMulti = 1.0f;
    public float parallaxLayerMulti;
    private float length;
    public RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        //length = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Rect pos y: " + rect.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //float newY = transform.position.y - 1.0f * parallaxLayerMulti * parallaxGroupMulti * Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        //if (transform.position.y < -length)
        //{
        //    transform.position = new Vector3(transform.position.x, length * 0.98f, transform.position.z);
        //}

        //float newY = rect.position.y - (1.0f * parallaxLayerMulti * parallaxGroupMulti * Time.deltaTime);
        float newY = rect.anchoredPosition.y - (100.0f * parallaxLayerMulti * parallaxGroupMulti * Time.deltaTime);
        //rect.position = new Vector3(rect.position.x, newY, rect.position.z);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);

        if (rect.anchoredPosition.y < -2092)
        {
            rect.position = new Vector3(rect.position.x, 0.0f, rect.position.z);
        }
    }

    public static void IncreaseSpeedMulti(float delta)
    {
        parallaxGroupMulti += delta;
    }
}
