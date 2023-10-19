using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int speed;
    public static Rect safeArea;

    // Start is called before the first frame update
    void Start()
    {
        safeArea = Screen.safeArea;    
    }

    // Update is called once per frame
    void Update()
    {
        float oldPosX = transform.position.x;
        bool isTouch = (Input.touchCount > 0) ? true : false;
        float movement = (isTouch ? 1 : -1) * speed * Time.deltaTime;
        float newX = oldPosX + movement;
        if (newX > -2.0f && newX < 2.0f)
            transform.Translate(movement, 0, 0);
        //if (newX > safeArea.x && newX < safeArea.width)
        //    transform.Translate(movement, 0, 0);
    }
}
