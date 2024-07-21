using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public InputField InputCharacterName;
    public Button ButtonMale;
    public Button ButtonFemale;
    public Button ConfirmButton;
    public Text PlayerNameText;
    public Image PlayerAvatarImage;
    private string selectedGender;

    void Start()
    {
        // Check if UI components are assigned
        if (InputCharacterName == null)
        {
            Debug.LogError("InputCharacterName is not assigned in the Inspector.");
        }
        if (ButtonMale == null)
        {
            Debug.LogError("ButtonMale is not assigned in the Inspector.");
        }
        if (ButtonFemale == null)
        {
            Debug.LogError("ButtonFemale is not assigned in the Inspector.");
        }
        if (ConfirmButton == null)
        {
            Debug.LogError("ConfirmButton is not assigned in the Inspector.");
        }

        // Add listeners for the gender buttons
        ButtonMale.onClick.AddListener(() => OnGenderButtonClick("M"));
        ButtonFemale.onClick.AddListener(() => OnGenderButtonClick("F"));
        ConfirmButton.onClick.AddListener(OnConfirmButtonClick);
    
        // Check if UI components for character display are assigned
        if (PlayerNameText == null || PlayerAvatarImage == null)
        {
            Debug.LogError("UI components for character display are not assigned in the Inspector.");
            return;
        }

        // Load and display character data if it exists
        LoadCharacterData();
    }

    private void OnGenderButtonClick(string gender)
    {
        selectedGender = gender;
        Debug.Log("Gender selected: " + gender);

        // Update button states
        if (gender == "M")
        {
            ButtonMale.interactable = false;
            ButtonFemale.interactable = true;
        }
        else if (gender == "F")
        {
            ButtonMale.interactable = true;
            ButtonFemale.interactable = false;
        }
    }

    private void OnConfirmButtonClick()
    {
        // Check if the input field and selected gender are valid
        if (InputCharacterName == null || string.IsNullOrEmpty(InputCharacterName.text))
        {
            Debug.LogError("Player name is not entered.");
            return;
        }

        if (string.IsNullOrEmpty(selectedGender))
        {
            Debug.LogError("Gender is not selected.");
            return;
        }

        // Save character data to pass to the next scene
        PlayerPrefs.SetString("PlayerName", InputCharacterName.text);
        PlayerPrefs.SetString("PlayerGender", selectedGender);
    
        // Load and display character data
        LoadCharacterData();
    }
    private void LoadCharacterData()
    {
        // Load character data from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName");
        string playerGender = PlayerPrefs.GetString("PlayerGender");

        if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerGender))
        {
            Debug.LogWarning("No character data found.");
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
