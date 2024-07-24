using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject escCanvas;
    public GameObject uiPanel;
    public GameObject inventoryPanel;
    public GameObject savePanel;
    public GameObject loadPanel;
    public GameObject confirmationDialog;
    public playerControl playerControl;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleESCMenu();
        }
    }

    public void ToggleESCMenu()
    {
        isPaused = !isPaused;
        escCanvas.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
            playerControl.SetMovement(false);
            ShowPanel(uiPanel); // Show UIPanel by default when ESC is pressed
        }
        else
        {
            Time.timeScale = 1f;
            playerControl.SetMovement(true);
            escCanvas.SetActive(false);
        }
    }

    public void ShowPanel(GameObject panel)
    {
        // Deactivate all panels
        uiPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);

        // Activate the desired panel
        panel.SetActive(true);
    }

    public void SaveDataButton()
    {
        ShowPanel(savePanel);

    }

    public void LoadDataButton()
    {
        ShowPanel(loadPanel);
    }

    public void BackToESCMenu(GameObject panel)
    {
        panel.SetActive(false);
        uiPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        escCanvas.SetActive(false);
        Time.timeScale = 1f;
        playerControl.SetMovement(true);
        isPaused = false;
    }

    public void ShowConfirmation(string message, System.Action onConfirm)
    {
        confirmationDialog.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure time scale is reset when returning to main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void OnHomeBtnClicked()
    {
        confirmationDialog.SetActive(true);
    }
}
