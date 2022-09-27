using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    Health playerHealth;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager; 
    public bool hasCompleted;

    void Awake() 
    {
        // Singleton pattern
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        
        if(numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        playerHealth = FindObjectOfType<Health>();
    }

    void Start()
    {
        // Set player score at 0
        scoreKeeper.ResetScore();

        // Set player health at initial health
        playerHealth.ResetHealth();
    }

    public void ProcessPlayerDeath() 
    {
        if(playerHealth.GetHealth() > 1) 
        {   
            playerHealth.ModifyHealth(-1);
            levelManager.RestartCurrentLevel();
        }
        else 
        {
            levelManager.LoadEndGame();
        }
    }

    public void ResetGameSession()
    {   
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }
        
        scoreKeeper.ResetScore();
        playerHealth.ResetHealth();
    } 
}
