using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : Character
{
    private int numHearts;
    public GameObject[] hearts;

    private SpriteRenderer spriteRenderer;
    private Color damageColour = Color.red;
    public float damageColourDuration;
    public float invincibilityDuration;
    private bool isInvincible;

    public float score;
    private int scoreToText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreEndText;

    public MenuSceneController menuController;
    private int movementTypeNum;
    private float gyroSensitivity = 0.05f;

    public GameObject gameActiveUI;
    public GameObject gameOverUI;
    public GameObject highScoreText;

    public enum moveType {
        touch,
        gyro
    }
    public moveType movementType;

    void Start()
    {
        gameActiveUI.SetActive(true);
        gameOverUI.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        SetHealthMax(this);
        numHearts = hearts.Length;
        scoreText.text = "0";

        movementTypeNum = PlayerPrefs.GetInt("MovementControls");
        movementType = (moveType)movementTypeNum;
        Debug.Log("Movement Type Number: " + movementTypeNum);

        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (movementType)
        {
            case moveType.touch:
                TouchMovement();
                break;
            case moveType.gyro:
                GyroMovement(); 
                break;
            default:
                TouchMovement();
                break;
        }

        if (health <= 0) {
            GameOver();
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
        Time.timeScale = 0;

        //menuController.GameOver();

        scoreEndText.text = scoreToText.ToString();

        gameOverUI.SetActive(true);
        gameActiveUI.SetActive(false);
        // In case movement type was changed during gameplay in editor
        PlayerPrefs.SetInt("MovementControls", GetMovementType());

        if (scoreToText > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreToText);
        } else
        {
            highScoreText.SetActive(false);
        }

        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("HighScore"));
        Debug.Log(scoreToText);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    void TouchMovement() 
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

    private void GyroMovement()
    {
        //transform.Translate(Input.acceleration.x * gyroSensitivity, 0, 0);

        // stores x position from previous frame
        float oldPosX = transform.position.x;
        // calculate change in position
        float movement = Input.acceleration.x * gyroSensitivity;// * Time.deltaTime;
        // stores x position after movement
        float newX = oldPosX + movement;
        // only applies movement if the new position is within the bounds
        if (newX > -2.0f && newX < 2.0f)
            transform.Translate(movement, 0, 0);
    }

    public int GetMovementType()
    {
        return (int)movementType;
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Is invincible in collider? " + isInvincible);
        // returns early if invincible
        if (isInvincible) return;

        // if colliding with object which is an enemy
        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            TakeDamage(1);
            StartCoroutine(InvincibilityFrames());
        }
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

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    private IEnumerator DamageColour()
    {
        spriteRenderer.color = damageColour;

        yield return new WaitForSeconds(damageColourDuration);

        spriteRenderer.color = Color.white;
    }
}
