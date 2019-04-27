using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource InGameAudio, MainMenuAudio, PlayerWinAudio, PlayerDieAudio, BatteryCollectAudio, ButtonSelectAudio;
    public bool isMute = false;

    public void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            InGameAudio.Play();
        }
        else
            MainMenuAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void PlayAutomatic(){

            switch (GameManager.CurrentGameState)
            {
                case GameState.MainMenu:
                    {
                        PauseAll();
                        MainMenuAudio.Play();
                        break;
                    }
                case GameState.Paused:
                    {
                        PauseAll();
                        MainMenuAudio.Play();
                        break;
                    }
                case GameState.Playing:
                    {
                        PauseAll();
                        InGameAudio.Play();
                        break;
                    }
                case GameState.Settings:
                    {
                        MainMenuAudio.Play();
                        break;
                    }

            }

    }

    public void StartGame()
    {
        if(MainMenuAudio.isPlaying){
            MainMenuAudio.Stop();
        }
        StartCoroutine(MakeItWait());
        InGameAudio.Play();
    }

    void PauseAll()
    {
        InGameAudio.Pause();
        MainMenuAudio.Pause();
        PlayerWinAudio.Pause();
        PlayerDieAudio.Pause();
        BatteryCollectAudio.Pause();
        ButtonSelectAudio.Pause();
    }

    IEnumerator MakeItWait()
    {
        print(Time.time);
        yield return new WaitForSeconds(1);
        print(Time.time);
    }

    public void buttonSelect()
    {
        ButtonSelectAudio.Play();
    }

    public void SetMute(bool muteStatus){
        isMute = muteStatus;
        InGameAudio.mute = muteStatus;
        MainMenuAudio.mute = muteStatus;
        PlayerWinAudio.mute = muteStatus;
        PlayerDieAudio.mute = muteStatus;
        BatteryCollectAudio.mute = muteStatus;
        ButtonSelectAudio.mute = muteStatus;

    }
}

public enum GameState
{
    MainMenu, Settings, Playing, Paused, Quit
};