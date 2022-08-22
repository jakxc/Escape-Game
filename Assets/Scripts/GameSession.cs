using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] float restartDelay = 2;
    
    [Header ("Player")]
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
   
    [Header ("UI")]
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;

    private void Awake() {
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
    }

    void Start()
    {
        livesText.text = "Lives left: " + playerLives.ToString();
        scoresText.text = "Score: " + playerScore.ToString();
    }

   public void ProcessPlayerDeath() 
   {
        if(playerLives > 1) 
        {   
            StartCoroutine(RestartCurrentLevel());
        }
        else 
        {
            StartCoroutine(ResetGameSession());
        }
   }

   public void IncrementScore(int value)
   {
        playerScore += value;
        scoresText.text = "Score: " + playerScore.ToString();
   }

    IEnumerator RestartCurrentLevel() 
    {   
        yield return new WaitForSecondsRealtime(restartDelay);

        playerLives--; 
        livesText.text = "Lives left: " + playerLives.ToString();
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator ResetGameSession()
    {   
        yield return new WaitForSecondsRealtime(restartDelay);

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }   
}
