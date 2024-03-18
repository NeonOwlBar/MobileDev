using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using FMODUnity;

public class PlayerControl : Character
{
    [Header("General")]
    private Rigidbody2D rb;
    private bool isTouch;
    public bool canMove;
    public MenuSceneController menuController;
    float screenBoundOffset;

    [Header ("Hearts Information")]
    public GameObject[] hearts;
    private int numHearts;

    [Header ("Player Damage")]
    [SerializeField] private EventReference damageSound;
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
    private int movementTypeNum;
    private float gyroSensitivity = 0.1f;

    [Header ("UI Game Objects")]
    public GameObject gameActiveUIObject;
    public GameUI gameActiveUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject highScoreText;

    [Header ("Cinemachine")]
    public CinemachineVirtualCamera virtualCam;
    public float camShakeIntensity = 1.0f;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    [Header("Power Ups")]
    public GameObject healthPrefab;

    [Header("Screen Bounds")]
    public static Rect safeArea;
    public float minX;
    public float maxX;

    [Header("Ads")]
    public InterstitialAd intAd;

    // types of movement
    public enum MoveType
    {
        touch,
        gyro
    }
    public MoveType movementType;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // set various UIs to active or not, depending on their use
        gameActiveUIObject.SetActive(true);
        gameActiveUI = gameActiveUIObject.GetComponent<GameUI>();
        pauseUI.SetActive(true);
        gameOverUI.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        // set max health for player
        SetHealthMax(this);
        // set number of hearts for UI
        numHearts = hearts.Length;
        // set the text for score ui to 0
        scoreText.text = "0";

        // assign values to safe area rect
        SetSafeArea();
        // create an offset for player screenbounds
        screenBoundOffset = (GetComponent<BoxCollider2D>().size.x) * 0.3f;
        // apply offset to the screenbounds
        minX += screenBoundOffset;
        maxX -= screenBoundOffset;

        // get current movement type
        movementTypeNum = PlayerPrefs.GetInt("MovementControls");
        // set current movement type
        movementType = (MoveType)movementTypeNum;
        Debug.Log("Movement Type Number: " + movementTypeNum);

        // allow gyro input
        Input.gyro.enabled = true;
        canMove = true;

        // set perlin noise reference
        perlinNoise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // resetting values that could have been changed or not reset in previous sessions
        ResetInvincibility();
        ResetPlayerColour();
        ResetShakeIntensity();

        // load an interstitial ad for when game ends
        intAd.LoadAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        isTouch = Input.touchCount > 0;

        switch (movementType)
        {
            case MoveType.touch:
                TouchMovement();
                //TouchMovement(isTouch);
                break;
            case MoveType.gyro:
                GyroMovement(); 
                break;
            default:
                TouchMovement();
                //TouchMovement(isTouch);
                break;
        }

        //SetTouchFalse();

        if (health <= 0)
        {
            ResetShakeIntensity();
            GameOver();
        }
        // Increments score at a fixed interval
        // mult'd by 1000 to allow a large enough increment, otherwise would not affect an int
        //score += 1000 * Time.fixedDeltaTime;
        score += 1000*Time.deltaTime;
        // divided by 1000 again and converted to int
        //scoreToText = (int)(score / 1000);
        scoreToText = (int)(score / 100);
        // Displayed as part of UI
        scoreText.text = scoreToText.ToString();

        // if score is a multiple of 100
        if (scoreToText % 100 == 0)
        {
            // create spawn point
            Vector3 randomPos = new Vector3(Random.Range(-1.75f, 1.75f), 7.0f, 0.0f);
            // instantiate heart at spawn point
            Instantiate(healthPrefab, randomPos, Quaternion.identity);

            // if score is below 4000
            if (scoreToText < 4000)
                // increase parallax speed
                Parallax.IncreaseSpeedMulti(0.02f);
        }
    }

    void GameOver()
    {
        menuController.GameOver();

        //WorldStates.GameOver();
        // removes player object from game scene
        gameObject.SetActive(false);
        //Time.timeScale = 0;

        scoreEndText.text = scoreToText.ToString();

        gameOverUI.SetActive(true);
        gameActiveUI.Deactivate();
        pauseUI.SetActive(false);

        // In case movement type was changed during gameplay in editor
        PlayerPrefs.SetInt("MovementControls", GetMovementType());

        if (scoreToText > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreToText);
        }
        else
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
        if (newX >= minX && newX <= maxX)
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
    }

    private void GyroMovement()
    {
        // stores x position from previous frame
        float oldPosX = transform.position.x;
        // calculate change in position
        float movement = Input.acceleration.x * gyroSensitivity;
        // stores x position after movement
        float newX = oldPosX + movement;
        // only applies movement if the new position is within the bounds
        //if (newX > -2.0f && newX < 2.0f)
        if (newX >= minX && newX <= maxX)
            transform.Translate(movement, 0, 0);
    }

    public int GetMovementType()
    {
        return (int)movementType;
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Is invincible in collider? " + isInvincible);

        // if CAN take damage && colliding with an enemy
        if (!isInvincible && other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int _damage)
    {
        // make damage sound
        //AudioManager.Instance.PlayOneShot(damageSound, transform.position);
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
        // set player to not take damage
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        ResetInvincibility();
    }
    private void ResetInvincibility()
    {
        // player can take damage again
        isInvincible = false;
    }

    private IEnumerator DamageColour()
    {
        // add red colour filter to clownfish
        spriteRenderer.color = damageColour;
        // keep this colour for the passed amount of time
        yield return new WaitForSeconds(damageDuration);

        ResetPlayerColour();
    }
    private void ResetPlayerColour()
    {
        //reset colour to normal
        spriteRenderer.color = Color.white;
    }

    private IEnumerator CameraShake(float intensity)
    {
        // set amplitude for screenshake
        perlinNoise.m_AmplitudeGain = intensity;
        // wait for time provided continuing screen shake
        yield return new WaitForSeconds(damageDuration);
        // reset shake
        ResetShakeIntensity();
    }
    private void ResetShakeIntensity()
    {
        // return amplitude to zero to remove screen shake
        perlinNoise.m_AmplitudeGain = 0.0f;
    }

    public void SetSafeArea()
    {
        // stores safe area data in new rect
        safeArea = Screen.safeArea;
        Debug.Log("Safe Area X = " + safeArea.x);
        Debug.Log("Safe Area width  = " + safeArea.width);

        // find where screen edge is in world space
        minX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.x, 0, 0)).x;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(safeArea.width, 0, 0)).x;
    }
}
