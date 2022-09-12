using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] float restartDelay = 2f;
    Health playerHealth;
    ScoreKeeper scoreKeeper;
    public bool hasCompleted;

    string END_GAME = "EndGame";

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
            StartCoroutine(RestartCurrentLevel());
        }
        else 
        {
            LoadEndGame();
        }
    }

    void LoadEndGame()
    {
        SceneManager.LoadScene(END_GAME);
    }

     IEnumerator RestartCurrentLevel() 
    {   
        yield return new WaitForSecondsRealtime(restartDelay);

        playerHealth.ModifyHealth(-1);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ResetGameSession()
    {   
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        FindObjectOfType<ScoreKeeper>().ResetScore();
        Destroy(gameObject);
    } 
}
