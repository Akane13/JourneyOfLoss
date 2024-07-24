using System.IO;
using UnityEngine;

public class SavePlayerInfo : MonoBehaviour
{
    public void SaveInfo(string playerName, string playerGender)
    {
        PlayerInfo playerInfo = new PlayerInfo
        {
            name = playerName,
            gender = playerGender
        };

        string json = JsonUtility.ToJson(playerInfo);
        string path = Application.persistentDataPath + "/playerInfo.json";
        File.WriteAllText(path, json);
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public string gender;
    }
}
