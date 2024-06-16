using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerDisplay : MonoBehaviour
{
    public Text PlayerNameText;
    public Image PlayerAvatarImage;


    void Start()
    {
        // Load character data from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName");
        string playerGender = PlayerPrefs.GetString("PlayerGender");

        if (PlayerNameText == null || PlayerAvatarImage == null)
        {
            Debug.LogError("UI components are not assigned in the Inspector.");
            return;
        }

        // Update UI with player data
        PlayerNameText.text = playerName;

        if (playerGender == "M")
        {
            PlayerAvatarImage.sprite = Resources.Load<Sprite>("male");
        }
        else if (playerGender == "F")
        {
            PlayerAvatarImage.sprite = Resources.Load<Sprite>("female");
        }
    }
}
