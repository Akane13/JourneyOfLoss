using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public Text scoreText;
    public Text timeText;
    public Canvas resultCanvas;
    public Text resultScoreText;
    public Text highestScoreText;
    public ScoreManager scoreManager;

    private float countdownTime = 30f;
    private bool isTimerRunning = true;

    private void Start()
    {
        UpdateTimerDisplay();
        resultCanvas.gameObject.SetActive(false);
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
        int highestScore = scoreManager.GetHighestScore();

        resultCanvas.gameObject.SetActive(true);
        resultScoreText.text = scoreManager.CurrentScore.ToString();
        highestScoreText.text = $"Highest Score: {highestScore}";
    }
}
