using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    public bool hideUI;
    GameSession gameSession;
    
    [Header("Lives")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

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
        healthText.enabled = !hideUI;
        scoreText.enabled = !hideUI;
   
        healthText.text = "Lives: " + playerHealth.GetHealth().ToString();
        scoreText.text = scoreKeeper.GetScore().ToString();
    }
}
