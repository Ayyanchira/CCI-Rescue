using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] GameObject GUI;
    
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.CurrentGameState)
        {
            case GameState.MainMenu:
                GUI.GetComponent<GUIManager>().ShowMainMenu();
                break;
            case GameState.Settings:
                GUI.GetComponent<GUIManager>().ShowSettingPage();
                break;
            case GameState.Playing:
                SceneManager.LoadScene("Level1");
                break;
            case GameState.Paused:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.CurrentGameState)
        {
            case GameState.MainMenu:
                GUI.GetComponent<GUIManager>().ShowMainMenu();
                break;
            case GameState.Settings:
                GUI.GetComponent<GUIManager>().ShowSettingPage();
                break;
            case GameState.Playing:
                SceneManager.LoadScene("Level1");
                break;
            case GameState.Paused:
                break;
        }
    }
}
