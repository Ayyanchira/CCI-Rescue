using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuItems, SettingPage, ControlsPage, PausePage, BackDrop;
    [SerializeField] GameObject AJCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSettingPage(){
        MainMenuItems.SetActive(false);
        PausePage.SetActive(false);
        SettingPage.SetActive(true);
        ControlsPage.SetActive(false);
        GameManager.CurrentGameState = GameState.Settings;
    }

    public void showControls(){
        MainMenuItems.SetActive(false);
        SettingPage.SetActive(true);
        PausePage.SetActive(false);
        ControlsPage.SetActive(true);
    }

    public void ShowMainMenu(){
        //ControlsPage.SetActive(false);
        ControlsPage.SetActive(false);
        SettingPage.SetActive(false);
        PausePage.SetActive(false);
        MainMenuItems.SetActive(true);
        BackDrop.SetActive(true);
        print("Showing main menu");
        GameManager.CurrentGameState = GameState.MainMenu;
    }

    public void ShowPauseMenu(){
        if (AJCanvas == null)
        {
            AJCanvas = GameObject.FindWithTag("AJCanvas");
        }

        AJCanvas.SetActive(false);
        SettingPage.SetActive(false);
        BackDrop.SetActive(true);
        PausePage.SetActive(true);
        MainMenuItems.SetActive(false);
        GameManager.CurrentGameState = GameState.Paused;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        GameManager.CurrentGameState = GameState.Playing;
        BackDrop.SetActive(false);
        SettingPage.SetActive(false);
        PausePage.SetActive(false);
        MainMenuItems.SetActive(false);

    }

    public void ResumeGame()
    {




        //AJCanvas = GameObject.FindWithTag("AJCanvas");
        //if(AJCanvas != null){
        //    AJCanvas.SetActive(true);
        //}else{
        //    print("AJ's canvas not found");
        //}
        AJCanvas.SetActive(true);

        Play();
    }
}
