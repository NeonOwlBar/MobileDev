using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header ("Character Values")]
    public int speed;
    public int health;
    public int maxHealth;

    public static Rect safeArea;

    void Start()
    {
        // stores safe area data in new rect
        safeArea = Screen.safeArea;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHealthMax(Character _object)
    {
        // sets health to value of maxHealth
        _object.health = maxHealth;
    }
}
