using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{   
    [SerializeField] float loadDelay = 2f;
    
    GameSession gameSession;

    void Awake() {
        gameSession = FindObjectOfType<GameSession>();    
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {   
        if (other.tag == "Player") 
        {
            StartCoroutine(LoadNextLevel());   
        }          
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        FindObjectOfType<ScenePersist>().ResetScenePersist();
       
        // Load next level
        SceneManager.LoadScene(nextSceneIndex);
        
        // Completed game after exiting last level
        if (nextSceneIndex ==  SceneManager.sceneCountInBuildSettings - 1)
        {
            gameSession.hasCompleted = true;
        }
    }
}
