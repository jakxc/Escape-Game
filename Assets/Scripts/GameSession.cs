using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    Health playerHealth;
    ScoreKeeper scoreKeeper;
    UIDisplay uiDisplay;
    LevelManager levelManager; 

    public bool hasCompleted;
    
    [SerializeField] int delay;

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

        uiDisplay = FindObjectOfType<UIDisplay>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        playerHealth = FindObjectOfType<Health>();
    }

    void Start() 
    {
        playerHealth.ResetHealth();
        scoreKeeper.ResetScore();
    }

    public IEnumerator ProcessPlayerDeath() 
    {
        yield return new WaitForSeconds(delay);

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
        Destroy(gameObject);
    } 
}
