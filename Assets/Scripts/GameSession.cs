using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] float reloadDelay = 1.5f;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            Invoke("TakeLife", reloadDelay);
        }
        else
        {
            Invoke("ResetGameSession", reloadDelay);
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePercist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives --;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
}
