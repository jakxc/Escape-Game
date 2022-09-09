using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteraction : MonoBehaviour
{
    string MAIN_MENU = "MainMenu";
    string LEVEL_ONE = "Level01";

    public void StartGame()
    {   
        SceneManager.LoadScene(LEVEL_ONE);
    }

    public void LoadMainMenu()
    {   
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void ExitGame()
    {
        Application.Quit();
    } 
}
