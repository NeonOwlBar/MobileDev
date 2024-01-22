using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public TextMeshProUGUI restartText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MenuSceneController.TextFade(restartText);
    }
}
