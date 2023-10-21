using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    // assigns effect to text
    public TextMeshProUGUI tapToStart;
    public GameObject mainMenuUI;

    //public TextMeshProUGUI tapToRestart;
    // value for change in alpha
    float deltaAlpha = 0.0f;

    // Update is called once per frame
    void Update()
    {
        TextFade(tapToStart);
    }

    // Adds a fading effect to text
    void TextFade(TextMeshProUGUI text)
    {
        // Increase deltaAlpha every second
        deltaAlpha += 2.0f * Time.deltaTime;
        // Set the text alpha to new value, manipulated to always be between 0 and 1
        tapToStart.alpha = (Mathf.Cos(deltaAlpha) + 1) / 2;
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsClicked()
    {
        mainMenuUI.SetActive(false);
        Debug.Log("Settings button clicked");
    }

    public void GameOver()
    {
        //TextFade(tapToRestart);
    }
}
