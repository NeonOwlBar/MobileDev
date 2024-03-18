using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // multiplier for ALL layers
    public static float parallaxGroupMulti = 1.0f;
    // multiplier for individual layer
    public float parallaxLayerMulti;
    // rect transform of each layer
    public RectTransform rect;

    // Start is called before the first frame update
    //void Start()
    //{
    //    Debug.Log("Rect pos y: " + rect.position.y);
    //}

    // Update is called once per frame
    void Update()
    {
        // calculate new y position for layer
        float newY = rect.anchoredPosition.y - (100.0f * parallaxLayerMulti * parallaxGroupMulti * Time.deltaTime);
        // set new position
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, newY);

        // if moved one image height down
        if (rect.anchoredPosition.y < -2092)
        {
            // return to original position
            // allows for smooth movement
            rect.position = new Vector3(rect.position.x, 0.0f, rect.position.z);
        }
    }

    public static void IncreaseSpeedMulti(float delta)
    {
        // increase speed of all layers
        parallaxGroupMulti += delta;
    }
}
