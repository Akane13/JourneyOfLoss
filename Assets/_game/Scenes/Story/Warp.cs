using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public Transform End1;
    public GameObject confirmPanel; // Reference to the confirmation panel
    private bool canTrigger = false; // To control the activation of the trigger

    private void Start()
    {
        confirmPanel.SetActive(false); // Hide the confirmation panel initially
        StartCoroutine(DelayTriggerActivation(10f)); // Start the coroutine to delay trigger activation for 10 seconds
    }

    private IEnumerator DelayTriggerActivation(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        canTrigger = true; // Allow the trigger to activate after the delay
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTrigger && other.CompareTag("Player")) // Check if the trigger can activate and the collider is the player
        {
            Debug.Log("Go Ending 1.");
            confirmPanel.SetActive(true); // Show the confirmation panel
        }
    }

    public void OnConfirmYes()
    {
        SceneManager.LoadScene("End1"); // Load the scene named "End1"
    }

    public void OnConfirmNo()
    {
        confirmPanel.SetActive(false); // Hide the confirmation panel
    }
}
