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

        switch (MenuSceneController.controlType)
        {
            case MenuSceneController.ControlSelection.Touch:
                ControlsToTouch();
                break;
            case MenuSceneController.ControlSelection.Gyroscope:
                ControlsToGyro();
                break;
            // if no data, default to touch controls
            default:
                ControlsToTouch();
                PlayerPrefs.SetInt("MovementControls", MenuSceneController.GetControlType());
                PlayerPrefs.Save();
                break;
        }

        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MenuSceneController.TextFade(tapToStart);
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
        switch (MenuSceneController.controlType)
        {
            case MenuSceneController.ControlSelection.Touch:
                ControlsToGyro();
                break;
            case MenuSceneController.ControlSelection.Gyroscope:
                ControlsToTouch();
                break;
        }
        PlayerPrefs.SetInt("MovementControls", MenuSceneController.GetControlType());
        PlayerPrefs.Save();
    }

    private void ControlsToGyro()
    {
        toggleControlsText.text = "Gyroscope";
        toggleImage.image.sprite = toggleBlue;
        MenuSceneController.controlType = MenuSceneController.ControlSelection.Gyroscope;
    }

    private void ControlsToTouch()
    {
        toggleControlsText.text = "Touch";
        toggleImage.image.sprite = toggleOrange;
        MenuSceneController.controlType = MenuSceneController.ControlSelection.Touch;
    }
}
