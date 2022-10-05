using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{   
    GameSession gameSession;
    LevelManager levelManager;
    UIDisplay uIDisplay;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        levelManager = FindObjectOfType<LevelManager>();
        uIDisplay = FindObjectOfType<UIDisplay>();   
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {   
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (other.tag == "Player") 
        {
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                gameSession.hasCompleted = true;
                levelManager.LoadEndGame();
            } 
            else 
            { 
                levelManager.LoadNextLevel(); 
            }
        }          
    }
}
