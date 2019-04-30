using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameState CurrentGameState;
    private GameObject GUI;

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
        GUI = GameObject.Find("GUI");
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEscKey();

    }

    private void LoadGame()
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
                StartGame();
                break;
            case GameState.Paused:
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    Debug.Log("Level1");
                }
                break;
        }
    }

    public void PlayGame(){

        StartGame();

    }

    private void CheckForEscKey()
    {
        if(Input.GetKey(KeyCode.Escape)){
            if (GUI == null)
            {
                print("GUI component not found...");
                return;
            }
            GUIManager guiManager = GUI.GetComponent<GUIManager>();

            if (CurrentGameState == GameState.Playing)
            {
                //Pause the game
                PauseGame();
                guiManager.ShowPauseMenu();

            }
            else
            {
                ResumeGame();
                guiManager.ResumeGame();
            }



        }
    }

    public void ResumeGame()
    {
        CurrentGameState = GameState.Playing;
        Time.timeScale = 1.0f;
        GUI.GetComponent<GUIManager>().ResumeGame();
    }

    public static void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        Time.timeScale = 0.0f;
    }

    public void StartGame(){
        CurrentGameState = GameState.Playing;
        if(SceneManager.GetActiveScene().name == "Menu")
            SceneManager.LoadScene("Level1");   
    }

    //public void EscButtonPressed()
    //{
    //    CurrentGameState = GameState.Paused;
    //}

    //public void ResumeGame(){
    //    CurrentGameState = GameState.Playing;
    //}



}