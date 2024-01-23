using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MenuSceneController : MonoBehaviour
{
    public InterstitialAd interstitialAd;

    // value for change in text alpha
    static float deltaAlpha = 0.0f;

    public enum ControlSelection
    {
        Touch,
        Gyroscope
    }

    public static ControlSelection controlType;

    // Adds a fading effect to text
    public static void TextFade(TextMeshProUGUI text)
    {
        // Increase deltaAlpha every second, even when time scale is zero (game logic paused)
        deltaAlpha += 2.0f * Time.unscaledDeltaTime;
        // Set the text alpha to new value, manipulated to always be between 0 and 1
        text.alpha = (Mathf.Cos(deltaAlpha) + 1) / 2;
    }

    

    public static int GetControlType()
    {
        return (int)controlType;
    }

    public void GameOver()
    {
        interstitialAd.LoadAd();
        //TextFade(tapToRestart);
    }
}
