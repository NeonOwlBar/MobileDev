using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int speed;
    public int health;
    public const int maxHealth = 100;

    public static Rect safeArea;

    // Start is called before the first frame update
    void Start()
    {
        // stores safe area data in new rect
        safeArea = Screen.safeArea;
        // sets health to value of maxHealth
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
