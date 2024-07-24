using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationDialog : MonoBehaviour
{
    public System.Action onConfirm;

    public void Confirm()
    {
        onConfirm?.Invoke();
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
