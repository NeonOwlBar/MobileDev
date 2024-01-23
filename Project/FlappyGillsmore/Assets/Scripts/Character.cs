using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header ("Character Values")]
    public float speed;
    public int health;
    public int maxHealth;




    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHealthMax(Character obj)
    {
        // sets health to value of maxHealth
        obj.health = maxHealth;
    }

    //public void SetSafeArea()
    //{
    //    // stores safe area data in new rect
    //    safeArea = Screen.safeArea;
    //    Debug.Log("Safe Area X = " + safeArea.x);
    //    Debug.Log("Safe Area width  = " + safeArea.width);

    //    // find where screen edge is in world space
    //    minX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.x, 0, 0)).x;
    //    maxX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.width, 0, 0)).x;
    //}
}
