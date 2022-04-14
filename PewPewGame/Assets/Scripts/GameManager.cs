using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupIndicator;
    public TextMeshProUGUI gameOverText;
    public string powerup;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateGameOver(false);
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public void UpdatePowerup(string powerup)
    {
        powerupIndicator.text = "Powerup : " + powerup; 
    }

    public void UpdateGameOver(bool gameOver)
    {
        if (gameOver)
        {
            gameOverText.text = "Game Over";
        }
    }
}
