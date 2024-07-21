using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : Singleton<UIController>
{
    public Text scoreText;
    public Text timeText;
    public Canvas resultCanvas;
    public Text resultScoreText;
    public Text highestScoreText;
    public ScoreManager scoreManager;
    public Canvas startCanvas;  // Reference to the start canvas
    public Canvas gameCanvas;   // Reference to the game canvas

    private float countdownTime = 60f;
    private bool isTimerRunning = true;

    private void Start()
    {
        UpdateTimerDisplay();
        resultCanvas.gameObject.SetActive(false);
        startCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                isTimerRunning = false;
                ShowResult();
            }
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void AddScore(int increment = 1)
    {
        scoreManager.AddScore(increment);
        scoreText.text = scoreManager.CurrentScore.ToString();
    }

    private void ShowResult()
    {
        scoreManager.SaveCurrentScore();
        resultCanvas.gameObject.SetActive(true);
        resultScoreText.text = scoreManager.CurrentScore.ToString();

        int highestScore = GetHighestScoreFromDatabase();
        if (scoreManager.CurrentScore > highestScore)
        {
            highestScore = scoreManager.CurrentScore;
            highestScoreText.text = $"Highest Score: {highestScore.ToString()}";
        }
        else
        {
            highestScoreText.text = $"Highest Score: {highestScore.ToString()}";
        }

        string conn = SetDataBaseClass.SetDataBase("player" + ".db");
        if (string.IsNullOrEmpty(conn))
        {
            Debug.LogError("Database connection string is null or empty");
            return;
        }

        using (IDbConnection dbcon = new SqliteConnection(conn))
        {
            try
            {
                dbcon.Open();
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO Score (Value) VALUES (@score)";
                    dbcmd.CommandText = sqlQuery;

                    var parameter = dbcmd.CreateParameter();
                    parameter.ParameterName = "@score";
                    parameter.Value = scoreManager.CurrentScore;
                    dbcmd.Parameters.Add(parameter);

                    dbcmd.ExecuteNonQuery();
                    Debug.Log("Score inserted successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Database operation failed: " + ex.Message);
            }
            dbcon.Close();
        }
    }

    private int GetHighestScoreFromDatabase()
    {
        int highestScore = 0;
        string conn = SetDataBaseClass.SetDataBase("player" + ".db");
        if (string.IsNullOrEmpty(conn))
        {
            Debug.LogError("Database connection string is null or empty");
            return highestScore;
        }

        using (IDbConnection dbcon = new SqliteConnection(conn))
        {
            try
            {
                dbcon.Open();
                using (IDbCommand dbcmd = dbcon.CreateCommand())
                {
                    string sqlQuery = "SELECT MAX(Value) FROM Score";
                    dbcmd.CommandText = sqlQuery;
                    Debug.Log($"Executing query: {dbcmd.CommandText}");
                    using (IDataReader reader = dbcmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            highestScore = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            Debug.Log($"Highest score retrieved: {highestScore}");
                        }
                        else
                        {
                            Debug.Log("No values found in the Score table.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Database operation failed: " + ex.Message);
            }
            dbcon.Close();
        }
        Debug.Log($"Highest score retrieved from database: {highestScore}");
        return highestScore;
    }

    public void OnClickStartGame()
    {
        startCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        isTimerRunning = true;  // Start the timer
    }
}
