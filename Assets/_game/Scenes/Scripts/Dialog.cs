using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Dialog : MonoBehaviour
{
    private Transform dialogControl;
    private List<DialogueLine> dialogLines = new List<DialogueLine>();
    private float dialogChangeTime = 0;
    private int index = -1;
    public Text dialogText;
    public Text nameText; // Shared text for player and NPC names
    public Image avatarImage; // Shared image for player and NPC avatars

    public string csvFilePath;
    private string playerName = "PlayerName"; // Default player name
    private string playerGender = "Male"; // Default player gender
    public Sprite maleAvatar;
    public Sprite femaleAvatar;
    public Sprite emilyAvatar;
    public Sprite georgeAvatar;

    public float typingSpeed = 0.05f; // Time between each character is typed
    public float changeTimeThreshold = 3.0f; // Time before automatically changing to the next line

    private bool isTyping = false; // To control typing effect

    [System.Serializable]
    public struct DialogueLine
    {
        public int id;
        public string avatar;
        public string character;
        public string text;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerInfo();
        LoadDialogFromCSV(csvFilePath);
        dialogControl = GetComponent<Transform>();
        dialogControl.GetChild(0).gameObject.SetActive(false); // Hide dialog panel at start
    }

    // Update is called once per frame
    void Update()
    {
        dialogChangeTime += Time.deltaTime;
        if (dialogChangeTime >= changeTimeThreshold && !isTyping)
        {
            ChangeText();
            dialogChangeTime = 0;
        }

        // Allow player to skip the typing animation
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogText.text = dialogLines[index].text;
                isTyping = false;
            }
            else if (index < dialogLines.Count)
            {
                ChangeText();
            }
        }
    }

    void LoadDialogFromCSV(string filePath)
    {
        List<Dictionary<string, string>> csvData = CSVReader.Read(filePath);

        foreach (var entry in csvData)
        {
            DialogueLine line;
            if (!int.TryParse(entry["ID"], out line.id))
            {
                Debug.LogError($"Invalid ID value: {entry["ID"]}");
                continue;
            }
            line.avatar = entry["Avatar"];
            line.character = entry["Character"];
            line.text = entry["Text"].Replace("(Username)", playerName).Trim('\"'); // Remove any surrounding quotes

            dialogLines.Add(line);
        }

        if (dialogLines.Count == 0)
        {
            Debug.LogError("No dialogue lines loaded. Please check your CSV file.");
        }
    }

    void LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/playerInfo.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
            playerName = playerInfo.name;
            playerGender = playerInfo.gender;
        }
        else
        {
            Debug.LogWarning("Player info file not found. Using default values.");
            playerName = "DefaultName"; // Default name when no saved data is found
            playerGender = "Male"; // Default gender when no saved data is found
        }

        UpdatePlayerInfo();
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public string name;
        public string gender;
    }

    void UpdatePlayerInfo()
    {
        nameText.text = playerName;

        if (playerGender == "Male")
        {
            avatarImage.sprite = maleAvatar;
        }
        else if (playerGender == "Female")
        {
            avatarImage.sprite = femaleAvatar;
        }
    }

    public void ChangeText()
    {
        if (index == -1)
        {
            index++;
        }
        else if (index >= dialogLines.Count)
        {
            dialogControl.GetChild(0).gameObject.SetActive(false); // Hide dialog panel at the end
        }
        else
        {
            string characterName = dialogLines[index].character;
            nameText.text = characterName == "(Username)" ? playerName : characterName;
            SetAvatar(characterName);
            dialogText.text = string.Empty;
            Debug.Log($"Displaying dialogue: {dialogLines[index].text}");
            StartCoroutine(TypeLine(dialogLines[index].text));

            if (index == 0) // Show dialog panel when the first line is actually displayed
            {
                dialogControl.GetChild(0).gameObject.SetActive(true);
            }

            // Debug information
            Debug.Log($"Current line ID: {dialogLines[index].id}");

            index++;
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        Debug.Log("Finished typing line.");
    }

    void SetAvatar(string characterName)
    {
        switch (characterName)
        {
            case "Emily":
                avatarImage.sprite = emilyAvatar;
                break;
            case "George Ruggs":
                avatarImage.sprite = georgeAvatar;
                break;
            case "(Username)":
                if (playerGender == "Male")
                {
                    avatarImage.sprite = maleAvatar;
                }
                else if (playerGender == "Female")
                {
                    avatarImage.sprite = femaleAvatar;
                }
                break;
            default:
                avatarImage.sprite = null;
                break;
        }
    }
}
