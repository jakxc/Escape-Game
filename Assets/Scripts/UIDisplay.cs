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

    void Awake() 
    {
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
        healthText.text = "Lives: " + playerHealth.GetHealth().ToString();
        scoreText.text = scoreKeeper.GetScore().ToString();
    }
}
