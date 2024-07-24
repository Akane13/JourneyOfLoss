using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    private string dataPath = "Assets/_game/Resources/UpdateData/saveData/";
    private string[] dataFiles = { "Data1.txt", "Data2.txt", "Data3.txt" };
    public Text playerNameText1;
    public Text saveTimeText1;
    public Text playerNameText2;
    public Text saveTimeText2;
    public Text playerNameText3;
    public Text saveTimeText3;

    private IntertintoDatabase database;

    private void Awake()
    {
        database = FindObjectOfType<IntertintoDatabase>();
        if (database == null)
        {
            Debug.LogError("IntertintoDatabase component not found in the scene!");
        }
        
        EnsureDirectoryExists(dataPath);
    }

    private void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    // Save Data 1
    public void SaveData1()
    {
        SaveDataToFile(0);
    }

    // Save Data 2
    public void SaveData2()
    {
        SaveDataToFile(1);
    }

    // Save Data 3
    public void SaveData3()
    {
        SaveDataToFile(2);
    }

    // Load Data 1
    public void LoadData1()
    {
        LoadDataFromFile(0);
    }

    // Load Data 2
    public void LoadData2()
    {
        LoadDataFromFile(1);
    }

    // Load Data 3
    public void LoadData3()
    {
        LoadDataFromFile(2);
    }

    private void SaveDataToFile(int slot)
    {
        if (slot < 0 || slot >= dataFiles.Length)
        {
            Debug.LogError("Invalid save slot.");
            return;
        }

        if (database == null)
        {
            database = FindObjectOfType<IntertintoDatabase>();
        }

        var lastRecord = database.GetLastRecord();
        string playerName = lastRecord.playerName;
        string gender = lastRecord.gender;

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPosition = playerTransform.position;

        string path = dataPath + dataFiles[slot];
        DeleteFile(path);

        string saveTime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        string data = DataJsonTest(saveTime, playerName, gender, playerPosition);

        CreateOrOpenFile(path, data);

        UpdateUIElements(slot, playerName, saveTime);

        Debug.Log($"Save Data {slot + 1}");
    }

    private void LoadDataFromFile(int slot)
    {
        if (slot < 0 || slot >= dataFiles.Length)
        {
            Debug.LogError("Invalid load slot.");
            return;
        }

        string path = dataPath + dataFiles[slot];
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found.");
            return;
        }

        string data = File.ReadAllText(path);
        SaveDataObject.DataForPlayer dataForPlayer = JsonUtility.FromJson<SaveDataObject.DataForPlayer>(data);
        
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.position = new Vector3(dataForPlayer.playerX, dataForPlayer.playerY, dataForPlayer.playerZ);

        UpdateUIElements(slot, dataForPlayer.name, dataForPlayer.dataSaveTime);

        Debug.Log($"Loaded Data {slot + 1}: {data}");
    }

    private void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Deleted file at {path}");
        }
    }

    private void CreateOrOpenFile(string path, string info)
    {
        using (StreamWriter sw = new FileInfo(path).CreateText())
        {
            sw.WriteLine(info);
            Debug.Log($"Created file at {path} with info: {info}");
        }
    }

    private string DataJsonTest(string saveTime, string playerName, string gender, Vector3 playerPosition)
    {
        SaveDataObject.DataForPlayer dataForPlayer = new SaveDataObject.DataForPlayer
        {
            name = playerName,
            gender = gender,
            playerX = playerPosition.x,
            playerY = playerPosition.y,
            playerZ = playerPosition.z,
            dataSaveTime = saveTime
        };

        return JsonUtility.ToJson(dataForPlayer);
    }

    private void UpdateUIElements(int slot, string playerName, string saveTime)
    {
        switch (slot)
        {
            case 0:
                if (playerNameText1 != null)
                {
                    playerNameText1.text = playerName;
                }
                else
                {
                    Debug.LogError("PlayerName Text component for slot 1 not found");
                }

                if (saveTimeText1 != null)
                {
                    saveTimeText1.text = saveTime;
                }
                else
                {
                    Debug.LogError("SaveTime Text component for slot 1 not found");
                }
                break;

            case 1:
                if (playerNameText2 != null)
                {
                    playerNameText2.text = playerName;
                }
                else
                {
                    Debug.LogError("PlayerName Text component for slot 2 not found");
                }

                if (saveTimeText2 != null)
                {
                    saveTimeText2.text = saveTime;
                }
                else
                {
                    Debug.LogError("SaveTime Text component for slot 2 not found");
                }
                break;

            case 2:
                if (playerNameText3 != null)
                {
                    playerNameText3.text = playerName;
                }
                else
                {
                    Debug.LogError("PlayerName Text component for slot 3 not found");
                }

                if (saveTimeText3 != null)
                {
                    saveTimeText3.text = saveTime;
                }
                else
                {
                    Debug.LogError("SaveTime Text component for slot 3 not found");
                }
                break;

            default:
                Debug.LogError("Invalid slot number");
                break;
        }
    }
}
