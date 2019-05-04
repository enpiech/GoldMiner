using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int score = 0;
    public int targetScore = 200;
    public Text scoreText;
    public int timePerLevel = 30;
    public GameObject youWon;
    public GameObject gameOver;

    private float clockSpeed = 1f;


    void Awake()
    {
        scoreText.text = ("Score: " + score + "/" + targetScore);
    }

    void CheckGameOver()
    {
        if (score >= targetScore)
        {
            Time.timeScale = 0;
            youWon.SetActive(true);
        } 
        else
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }

    public void AddPoint(int pointScored)
    {
        score += pointScored;
        scoreText.text = ("Score: " + score + "/" + targetScore);
    }
}
