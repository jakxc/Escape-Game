using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI endGameText;
    ScoreKeeper scoreKeeper;
    GameSession gameSession;

    void Awake() 
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        if(gameSession.hasCompleted)
        {
            endGameText.text = "Congratulations!";
            finalScoreText.text = "You scored: " + scoreKeeper.GetScore().ToString();
        } 
        else
        {
            endGameText.text = "Better luck next time.";
            finalScoreText.text = "You scored: " + scoreKeeper.GetScore().ToString();
        }
    }
}
