using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // player
    public PlayerControl player;

    // UI
    public GameObject pauseUI;

    public GameObject pauseButton;

    public GameObject resumeButton;
    public TextMeshProUGUI resumeText;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.SetActive(false);
        pauseUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f)
        {
            MenuSceneController.TextFade(resumeText);
        }
    }

    public void PauseGame()
    {
        player.canMove = false;
        Time.timeScale = 0.0f;
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void UnpauseGame()
    {
        player.canMove = true;
        Time.timeScale = 1.0f;
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
