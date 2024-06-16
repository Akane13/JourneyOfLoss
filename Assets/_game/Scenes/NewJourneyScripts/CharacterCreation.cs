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
    }
}
