using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class IntertintoDatabase : MonoBehaviour
{
    public string DataBaseName;
    public InputField UsernameInput;
    public string Gender = "M"; // Default to Male
    public int Score;
    public ScoreManager scoreManager;
    public int insertscore;

    public void SetGenderMale()
    {
        Gender = "M";
    }

    public void SetGenderFemale()
    {
        Gender = "F";
    }

    public void InsertInto()
    {
        var _UsernameInput = UsernameInput.text.Trim();

        string conn = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;

        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string sqlQuery = $"INSERT INTO Player (Name, Gender) VALUES ('{_UsernameInput}' , '{Gender}')";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        reader.Close();
        dbcmd.Dispose();
        dbcon.Close();

        SavePlayerInfoToJson(_UsernameInput, Gender);
    }

    public (string playerName, string gender) GetLastRecord()
    {
        string conn = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbcon;
        IDbCommand dbcmd;
        IDataReader reader;
        string lastPlayerName = "";
        string lastGender = "";

        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
        string sqlQuery = "SELECT Name, Gender FROM Player ORDER BY id DESC LIMIT 1";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        
        if (reader.Read())
        {
            lastPlayerName = reader["Name"].ToString();
            lastGender = reader["Gender"].ToString();
        }

        reader.Close();
        dbcmd.Dispose();
        dbcon.Close();

        return (lastPlayerName, lastGender);
    }
    private void SavePlayerInfoToJson(string playerName, string gender)
    {
        PlayerInfo playerInfo = new PlayerInfo
        {
            name = playerName,
            gender = gender == "M" ? "Male" : "Female"
        };

        string json = JsonUtility.ToJson(playerInfo);
        string path = Application.persistentDataPath + "/playerInfo.json";
        File.WriteAllText(path, json);

        Debug.Log("Player info saved to " + path);
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public string gender;
    }
}
