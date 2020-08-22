using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public string hint;
    public Text hintText;

    public bool canPlayerInteract;

    public void OnHoverStart()
    {
        this.hintText.text = this.hint;
        this.hintText.enabled = true;
    }

    public void OnHoverStop()
    {
        this.hintText.enabled = false;
    }

    public void OnInteract()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("LevelChooser");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.canPlayerInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.canPlayerInteract = false;
        }
    }
}
