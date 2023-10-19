using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    // assigns effect to text
    public TextMeshProUGUI tapToStart;
    // value for change in alpha
    float deltaAlpha = -0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gradually adds deltaAlpha value to alpha value of text
        tapToStart.alpha += Mathf.Sin(deltaAlpha/3);
        if (tapToStart.alpha >= 1.0f || tapToStart.alpha <= 0.0f) 
        {   // reverses direction of change in alpha once min or max value
            deltaAlpha *= -1;
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Game");
    }
}
