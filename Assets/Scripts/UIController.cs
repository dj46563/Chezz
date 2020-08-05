using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class UIController
{
    public event Action MainMenuUnloaded;
    
    public UIController()
    {
        MainMenuController.HostPressed += UnloadMainMenu;
        MainMenuController.PlayPressed += UnloadMainMenu;
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void UnloadMainMenu()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        MainMenuUnloaded?.Invoke();
    }
}
