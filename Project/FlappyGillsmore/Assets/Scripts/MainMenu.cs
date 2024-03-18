using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // menu GOs
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;

    // assigns effect to text
    public TextMeshProUGUI tapToStart;
    // put in a game over menu script
    //public TextMeshProUGUI tapToRestart;
    public TextMeshProUGUI toggleControlsText;
    public Button toggleImage;
    public Sprite toggleOrange;
    public Sprite toggleBlue;

    // Start is called before the first frame update
    void Start()
    {
        // check if there are pre-existing settings for movement controls
        if (PlayerPrefs.HasKey("MovementControls"))
        {
            // convert the int to a ControlSelection
            MenuSceneController.controlType = (MenuSceneController.ControlSelection)PlayerPrefs.GetInt("MovementControls");
        }

        // depending on current movement type
        switch (MenuSceneController.controlType)
        {
            case MenuSceneController.ControlSelection.Touch:
                // set controls to use touch
                ControlsToTouch();
                break;
            case MenuSceneController.ControlSelection.Gyroscope:
                // set controls to use gyro
                ControlsToGyro();
                break;
            // if no data, default to touch controls
            default:
                ControlsToTouch();
                PlayerPrefs.SetInt("MovementControls", MenuSceneController.GetControlType());
                PlayerPrefs.Save();
                break;
        }
        //start at main menu
        mainMenuUI.SetActive(true);
        // hide options menu
        optionsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // fade text via alpha value
        MenuSceneController.TextFade(tapToStart);
    }

    public void OnStartClicked()
    {
        // set game to playing
        //WorldStates.isGamePlaying = true;
        // load game scene
        SceneManager.LoadScene("Game");
    }

    public void OnSettingsClicked()
    {
        // remove main menu
        mainMenuUI.SetActive(false);
        // show options menu
        optionsMenuUI.SetActive(true);
    }

    public void OnBackClicked()
    {
        // show main menu
        mainMenuUI.SetActive(true);
        // remove options menu
        optionsMenuUI.SetActive(false);
    }

    public void OnControlToggleClicked()
    {
        // checks current status of controls, switches to other type
        switch (MenuSceneController.controlType)
        {
            case MenuSceneController.ControlSelection.Touch:
                ControlsToGyro();
                break;
            case MenuSceneController.ControlSelection.Gyroscope:
                ControlsToTouch();
                break;
        }
        // save current settings
        PlayerPrefs.SetInt("MovementControls", MenuSceneController.GetControlType());
        PlayerPrefs.Save();
    }

    private void ControlsToGyro()
    {
        // set UI text to gyroscope
        toggleControlsText.text = "Gyroscope";
        // set UI toggle colour to blue
        toggleImage.image.sprite = toggleBlue;
        // set control type in enum
        MenuSceneController.controlType = MenuSceneController.ControlSelection.Gyroscope;
    }

    private void ControlsToTouch()
    {
        // set UI text to touch
        toggleControlsText.text = "Touch";
        // set UI toggle colour to orange
        toggleImage.image.sprite = toggleOrange;
        // set control type in enum
        MenuSceneController.controlType = MenuSceneController.ControlSelection.Touch;
    }
}
