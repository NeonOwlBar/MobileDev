using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Character
{
    public PlayerHealth playerHealth;
    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (health <= 0) {
            GameOver(); // to be implemented
        }
    }

    void GameOver()
    {
        this.gameObject.SetActive(false);
    }

    void PlayerMove() 
    {
        // stores x position from previous frame
        float oldPosX = transform.position.x;
        // true if touch input currently detected, false if not
        bool isTouch = (Input.touchCount > 0) ? true : false;
        // if touch: move right, else move left
        float movement = (isTouch ? 1 : -1) * speed * Time.deltaTime;
        // stores x position after movement
        float newX = oldPosX + movement;
        // only applies movement if the new position is within the bounds
        if (newX > -2.0f && newX < 2.0f)
            transform.Translate(movement, 0, 0);
        
        //if (newX > safeArea.x && newX < safeArea.width)
        //    transform.Translate(movement, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if colliding with object which is an enemy
        if (other.tag == "Enemy")
            health -= 10;
            Destroy(other.gameObject);
            Debug.Log("Player health = " + health);
            playerHealth.UpdateHealth();
    }
}
