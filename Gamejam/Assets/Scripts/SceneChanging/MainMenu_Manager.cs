using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Manager : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(SceneNavigation_Manager.instance.GoToNextScene);
        quitButton.onClick.AddListener(Application.Quit);
    }
    
}
