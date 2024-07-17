using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DB
{
    private SQLiteConnection _connection;

    public DB()
    {
        string DatabaseName = "player.db";

        // Use a straightforward path format for PC platforms
        var dbPath = Path.Combine(Application.streamingAssetsPath, DatabaseName);

        // Check if the database file exists in the StreamingAssets directory
        if (!File.Exists(dbPath))
        {
            Debug.LogError("Database not found in StreamingAssets path.");
            return;  // Exit the constructor if the database does not exist
        }

        // Initialize the SQLite connection
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Database path: " + dbPath);
    }

    public SQLiteConnection GetConnection()
    {
        return _connection;
    }
}