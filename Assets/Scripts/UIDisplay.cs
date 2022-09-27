using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    GameSession gameSession;

    void Awake() 
    {
        gameSession = FindObjectOfType<GameSession>();
        playerHealth = FindObjectOfType<Health>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }

    // Start is called before the first frame update
    void Start()
    {
       healthText.text = "Lives: " + playerHealth.GetHealth().ToString();
       scoreText.text = scoreKeeper.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameSession.hasCompleted)
        {
            healthText.enabled = false;
            scoreText.enabled = false;
        } 
        else
        {
            healthText.text = "Lives: " + playerHealth.GetHealth().ToString();
            scoreText.text = scoreKeeper.GetScore().ToString();
        }
    }
}
