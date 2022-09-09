using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int currentScore = 0;

    static ScoreKeeper instance;

    void Awake() 
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false); //Ensure ScoreKeeper is not used by other classes
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ModifyScore(int value)
    {
        currentScore += value;
        Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
