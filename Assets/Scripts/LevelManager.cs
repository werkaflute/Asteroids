using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int speedRaisingDifficulty;
    public GameObject player;
    public Text textScore;
    public GameObject credits;
    public Text endScore;
    public GameObject asteroidManager;
    public float projectileSpeed;
    public float minAsteroidSpeed, maxAsteroidSpeed;
    public float asteroidDecentralization;
    private int score = 0;
    private int speedRaiser = 5;
    private bool gameHasEnded = false;   

    public float GetRandomAsteroidSpeed()
    {
        return Random.Range(minAsteroidSpeed, maxAsteroidSpeed);
    }

  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(speedRaiser > 75)
        {
            speedRaiser = 0;
            minAsteroidSpeed += speedRaisingDifficulty;
            maxAsteroidSpeed += speedRaisingDifficulty;
        }

        if ((gameHasEnded) && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore()
    {
        score++;
        speedRaiser++;
    }

    public void AddScore(int value)
    {
        score += value;
        speedRaiser += value;
    }

    public void EndGame()
    {
        CancelInvoke();
        endScore.text = "Final Score: " + score;
        credits.SetActive(true);
        Destroy(asteroidManager);
        gameHasEnded = true;
    }

    private void Start()
    {
        InvokeRepeating("AddScore", 0, 1);
    }

    private void FixedUpdate()
    {
        textScore.text = "Score: " + score;
    }
}
