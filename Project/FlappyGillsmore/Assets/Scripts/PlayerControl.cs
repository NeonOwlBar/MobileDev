using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerControl : Character
{
    [Header("General")]
    private Rigidbody2D rb;

    [Header ("Hearts Information")]
    public GameObject[] hearts;
    private int numHearts;

    [Header ("Player Damage")]
    public float damageDuration;
    public float invincibilityDuration;
    private bool isInvincible;
    private SpriteRenderer spriteRenderer;
    private Color damageColour = Color.red;

    [Header ("Score")]
    public float score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreEndText;
    private int scoreToText;

    [Header ("Menu Controller")]
    public MenuSceneController menuController;
    private int movementTypeNum;
    private float gyroSensitivity = 0.05f;

    [Header ("UI Game Objects")]
    public GameObject gameActiveUI;
    public GameObject gameOverUI;
    public GameObject highScoreText;

    [Header ("Cinemachine")]
    public CinemachineVirtualCamera virtualCam;
    public float camShakeIntensity = 1.0f;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    public enum moveType {
        touch,
        gyro
    }
    public moveType movementType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // resetting values that could have been changed or not reset in previous sessions
        ResetInvincibility();
        ResetPlayerColour();
        ResetShakeIntensity();
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
        bool isTouch = Input.touchCount > 0;
        // if touch: move right, else move left
        float movement = (isTouch ? 1 : -1) * speed * Time.deltaTime;
        // stores x position after movement
        float newX = oldPosX + movement;
        
        // only applies movement if the new position is within the bounds
        if (newX > -2.0f && newX < 2.0f)
        {
            // new velocity vector
            Vector2 velocity = new Vector2(movement, 0);
            // convert to unit vector
            velocity.Normalize();
            // scale to speed
            velocity *= speed;
            // assign value to velocity of rb
            rb.velocity = velocity;
        }
        else
        {
            // movement would put player beyond screen bound, so don't apply movement
            rb.velocity = Vector2.zero;
        }
        
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
        // Player will not take damage while coroutine is running
        StartCoroutine(InvincibilityFrames());
        // Flashes red colour on character
        StartCoroutine(DamageColour());
        // creates camera shake on damage taken
        StartCoroutine(CameraShake(camShakeIntensity));
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        ResetInvincibility();
    }
    private void ResetInvincibility()
    {
        isInvincible = false;
    }

    private IEnumerator DamageColour()
    {
        spriteRenderer.color = damageColour;

        yield return new WaitForSeconds(damageDuration);

        ResetPlayerColour();
    }
    private void ResetPlayerColour()
    {
        spriteRenderer.color = Color.white;
    }

    private IEnumerator CameraShake(float intensity)
    {
        perlinNoise.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(damageDuration);

        ResetShakeIntensity();
    }
    private void ResetShakeIntensity()
    {
        perlinNoise.m_AmplitudeGain = 0.0f;
    }
}
