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
    public TextMeshProUGUI tapToRestart;
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
        // Set the text alpha to new value
        // PingPong sets value between 0 and the value in the second parameter
        text.alpha = 0.1f + Mathf.PingPong(Time.time / 1.5f, 0.9f);   // alpha between 0.1 and 1
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
