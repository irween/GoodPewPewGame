using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // public variables
    public int score;
    public int currentWave;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupIndicator;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI waveText;
    public string powerup;

    // Start is called before the first frame update
    void Start()
    {
        // setting the score to 0, and the gameover screen to false
        score = 0;
        UpdateGameOver(false);
        UpdateScore(0);
        UpdateWave(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this function updates the score by a certain amount whenever it is called by another script
    // parameters - scoreToAdd int this dictates the value added to the score then represented by the text
    // return value - none
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    // this function increases the wave when another script calls the function with a provided integer
    public void UpdateWave(int wave)
    {
        currentWave += wave;
        waveText.text = "Wave : " + currentWave;
    }

    // this function updates the powerup when called from another script with a string
    // parameters - powerup string this changes the text to correspond to the current powerup provided by the shooting script
    // return value - none
    public void UpdatePowerup(string powerup)
    {
        powerupIndicator.text = "Powerup : " + powerup; 
    }

    // this function updates the gameover boolean when called by another script (when the player is destroyed)
    // parameters - gameOver bool this sets the game over text to true and displays "Game Over"
    // return value - none
    public void UpdateGameOver(bool gameOver)
    {
        if (gameOver)
        {
            gameOverText.text = "Game Over";
        }
    }
}
