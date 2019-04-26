using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameState CurrentGameState;
    // Start is called before the first frame update

    void Start()
    {
        CurrentGameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEscKey();   
    }

    private void CheckForEscKey()
    {
        if(Input.GetKey(KeyCode.Escape)){
            CurrentGameState = GameState.Paused;
        }
    }


    public void StartGame(){
        CurrentGameState = GameState.Playing;
    }

    public void EscButtonPressed()
    {
        CurrentGameState = GameState.Paused;
    }

    public void ResumeGame(){
        CurrentGameState = GameState.Playing;
    }



}