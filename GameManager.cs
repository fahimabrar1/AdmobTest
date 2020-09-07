using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool gameOver = false;
    int score = 0;
    public Text scoreText;
    public Text scoreTextPanel;
    public Text highScoreText;

    public GameObject gameOverPanel;
    public GameObject pauseButton;

    private int adTurns;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        getAdTurns();
    }
    public void GameOver()
    {
        
        gameOver = true;

        if (adTurns<1)
        {
            getAdTurns();
            ShowFullScreenAd();
        }
        else
        {
            adTurns--;
        }

        pauseButton.SetActive(false);
        GameObject.Find("EnemySpawn").GetComponent<EnemySpawner>().StopSpawning();
        scoreTextPanel.text = "Score: " + score;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + score;
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }

        gameOverPanel.SetActive(true);
        PlayerDieSound.playSound();


    }

    public void ShowFullScreenAd()
    {

        AdMob.instance.bannerView.Destroy();
        AdMob.instance.ShowInterstitial();
    }


    public void getAdTurns()
    {
        adTurns = Random.Range(1, 4);
    }

    public void IncrementScore()
    {
        if (!gameOver)
        {
            score++;

            scoreText.text = score.ToString();
        }
        
    }

    public void MainMenu()
    {
        ButtonClickSound.playSound();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        ButtonClickSound.playSound();
        SceneManager.LoadScene("Game");
    }

}
