using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameButtonClick()
    {
        // Load the Create Character scene
        SceneManager.LoadScene("NewJourney");
    }
}
