using UnityEngine;
using SQLite4Unity3d;

public class ScoreManager : MonoBehaviour
{
    private DB dB;
    public int CurrentScore { get; private set; }

    private void Awake()
    {
        dB = new DB();
        CreateScoreTable();
    }

    private void CreateScoreTable()
    {
        using (var connection = dB.GetConnection())
        {
            try
            {
                var result = connection.ExecuteScalar<int>("SELECT count(name) FROM sqlite_master WHERE type='table' AND name='Score';");
                if (result == 0)
                {
                    connection.CreateTable<Score>();
                }
            }
            catch (SQLiteException ex)
            {
                Debug.LogError("Failed to create table: " + ex.Message);
            }
        }
    }

    public void AddScore(int scoreValue)
    {
        CurrentScore += scoreValue;
    }

    public void SaveCurrentScore()
    {
        try
        {
            using (var connection = dB.GetConnection())
            {
                Score score = new Score { Value = CurrentScore };
                connection.Insert(score);
            }
        }
        catch (SQLiteException ex)
        {
            Debug.LogError("Failed to insert score: " + ex.Message);
        }
    }

    public int GetHighestScore()
    {
        try
        {
            using (var connection = dB.GetConnection())
            {
                var query = connection.Table<Score>().OrderByDescending(s => s.Value).FirstOrDefault();
                return query != null ? query.Value : 0;
            }
        }
        catch (SQLiteException ex)
        {
            Debug.LogError("Failed to retrieve highest score: " + ex.Message);
            return 0;
        }
    }
}
