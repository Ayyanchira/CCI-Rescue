using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameState CurrentGameState;
    [SerializeField] GameObject GUI;

    public void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Game Manager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update

    void Start()
    {
        CurrentGameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEscKey();
        switch (GameManager.CurrentGameState)
        {
            case GameState.MainMenu:
                GUI.GetComponent<GUIManager>().ShowMainMenu();
                break;
            case GameState.Settings:
                GUI.GetComponent<GUIManager>().ShowSettingPage();
                break;
            case GameState.Playing:
                StartGame();
                break;
            case GameState.Paused:
                if(SceneManager.GetActiveScene().name == "Level1")
                {
                    Debug.Log("Level1");
                }
                break;
        }
    }

    private void CheckForEscKey()
    {
        if(Input.GetKey(KeyCode.Escape)){
            CurrentGameState = GameState.Paused;
        }
    }


    public void StartGame(){
        CurrentGameState = GameState.Playing;
        SceneManager.LoadScene("Level1");
    }

    public void EscButtonPressed()
    {
        CurrentGameState = GameState.Paused;
    }

    public void ResumeGame(){
        CurrentGameState = GameState.Playing;
    }



}