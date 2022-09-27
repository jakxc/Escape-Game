using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{   
    LevelManager levelManager;
    GameSession gameSession;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        levelManager = FindObjectOfType<LevelManager>();    
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {   
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (other.tag == "Player") 
        {
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                levelManager.LoadEndGame();
                gameSession.hasCompleted = true;
            } 
            else
            {
                levelManager.LoadNextLevel(); 
            } 
        }          
    }
}
