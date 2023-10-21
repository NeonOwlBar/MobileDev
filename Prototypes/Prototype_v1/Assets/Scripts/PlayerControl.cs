using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : Character
{
    private int numHearts;
    public GameObject[] hearts;

    private SpriteRenderer spriteRenderer;
    private Color damageColour = Color.red;
    public float damageDuration;

    public float score;
    private int scoreToText;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetHealthMax(this);
        numHearts = hearts.Length;
        scoreText.text = "0";    // check this
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (health <= 0) {
            GameOver(); // to be implemented
        }
        // Increments score at a fixed interval
        // mult'd by 1000 to allow a large enough increment, otherwise would not affect an int
        score += 1000*Time.fixedDeltaTime;
        // divided by 1000 again and converted to int
        scoreToText = (int)(score / 1000);
        // Displayed as part of UI
        scoreText.text = scoreToText.ToString();
    }

    void GameOver()
    {
        // removes player object from game scene
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
            Destroy(other.gameObject);
            TakeDamage(1);
    }

    void TakeDamage(int _damage)
    {
        // reduces health
        health -= _damage;
        // reduces value for number of active hearts in array
        numHearts -= _damage;
        // Removes corresponding heart from game scene
        hearts[health].gameObject.SetActive(false);
        // Adds corresponding empty heart to scene
        hearts[numHearts].gameObject.SetActive(true);
        // Flashes red colour on character
        StartCoroutine(DamageColour());
    }

    IEnumerator DamageColour()
    {
        spriteRenderer.color = damageColour;

        yield return new WaitForSeconds(damageDuration);

        spriteRenderer.color = Color.white;
    }
}
