using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    GameSession gameSession;
    UIDisplay uiDisplay;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        uiDisplay = FindObjectOfType<UIDisplay>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void RestartCurrentLevel() 
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
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

        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            uiDisplay.hideUI = true;
        }

        StartCoroutine(WaitAndLoad(nextSceneIndex, sceneLoadDelay));
    }

    public void StartGame()
    {
        uiDisplay.hideUI = false;

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }
      
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        gameSession.ResetGameSession();
        
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }

        SceneManager.LoadScene(0);
    }

    public void LoadEndGame()
    {
        int endGameIndex = SceneManager.sceneCountInBuildSettings - 1; 

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
        }
        
        StartCoroutine(WaitAndLoad(endGameIndex, sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (sceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            uiDisplay.hideUI = true;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}

