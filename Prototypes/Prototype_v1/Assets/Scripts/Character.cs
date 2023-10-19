using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int speed;
    public int health;
    public int maxHealth;

    public static Rect safeArea;

    // Start is called before the first frame update
    void Start()
    {
        safeArea = Screen.safeArea;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
