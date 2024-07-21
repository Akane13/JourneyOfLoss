using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using System;
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
        // Normally, you should use ExecuteNonQuery for INSERT, not ExecuteReader
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }

}
