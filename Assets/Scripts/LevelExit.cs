using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{   
    [SerializeField] float loadDelay = 2f;
    
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
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        // Load next level
        SceneManager.LoadScene(nextSceneIndex);
    }
}
