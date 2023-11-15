using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour
{
    // assigns effect to text
    public TextMeshProUGUI tapToStart;
    public GameObject mainMenuUI;

    public GameObject optionsMenuUI;
    public TextMeshProUGUI toggleControlsText;
    public Button toggleImage;
    public Sprite toggleOrange;
    public Sprite toggleBlue;

    public enum controlSelection {
        Touch,
        Gyroscope
    }

    public controlSelection controlType;

    //public TextMeshProUGUI tapToRestart;
    // value for change in alpha
    float deltaAlpha = 0.0f;

    void Start()
    {
        if (PlayerPrefs.HasKey("MovementControls")) {
            controlType = (controlSelection)PlayerPrefs.GetInt("MovementControls");
        }
        switch (controlType){
            case controlSelection.Touch:
                ControlsToTouch();
                break;
            case controlSelection.Gyroscope:
                ControlsToGyro();
                break;
        }
    
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TextFade(tapToStart);
    }

    // Adds a fading effect to text
    public void TextFade(TextMeshProUGUI text)
    {
        // Increase deltaAlpha every second
        deltaAlpha += 2.0f * Time.unscaledDeltaTime;
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
        optionsMenuUI.SetActive(true);
    }

    public void OnBackClicked() 
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void OnControlToggleClicked()
    {
        // checks current status of controls, switches to other type
        switch (controlType){
            case controlSelection.Touch:
                ControlsToGyro();
                break;
            case controlSelection.Gyroscope:
                ControlsToTouch();
                break;
        }
        PlayerPrefs.SetInt("MovementControls", GetControlType());
        PlayerPrefs.Save();
    }

    public int GetControlType()
    {
        return (int)controlType;
    }

    private void ControlsToGyro()
    {
        toggleControlsText.text = "Gyroscope";
        toggleImage.image.sprite = toggleBlue;
        controlType = controlSelection.Gyroscope;
    }

    private void ControlsToTouch()
    {
        toggleControlsText.text = "Touch";
        toggleImage.image.sprite = toggleOrange;
        controlType = controlSelection.Touch;
    }

    public void GameOver()
    {
        //TextFade(tapToRestart);
    }
}
