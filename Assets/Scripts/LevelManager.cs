using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    GameSession gameSession;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        // Singleton pattern
        int numberOfLevelManagers = FindObjectsOfType<LevelManager>().Length;
        
        if(numberOfLevelManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RestartCurrentLevel() 
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(WaitAndLoad(currentSceneIndex, sceneLoadDelay));
    }

    public void LoadNextLevel()
    {  
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }

        StartCoroutine(WaitAndLoad(nextSceneIndex, sceneLoadDelay));
    }

    public void StartGame()
    {
        gameSession.ResetGameSession();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadEndGame()
    {
        int endGameIndex = SceneManager.sceneCountInBuildSettings - 1; 
        StartCoroutine(WaitAndLoad(endGameIndex, sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneIndex);
    }
}

