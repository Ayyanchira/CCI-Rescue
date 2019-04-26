using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuItems, SettingPage, ControlsPage;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSettingPage(){
        MainMenuItems.SetActive(false);
        SettingPage.SetActive(true);
    }

    public void showControls(){
        MainMenuItems.SetActive(false);
        SettingPage.SetActive(true);
    }

    public void ShowMainMenu(){
        //ControlsPage.SetActive(false);
        SettingPage.SetActive(false);
        MainMenuItems.SetActive(true);
    }

    
}
