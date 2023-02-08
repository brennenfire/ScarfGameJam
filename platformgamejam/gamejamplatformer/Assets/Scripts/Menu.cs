using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

    Canvas menu;
    bool isOpen = false;

    void Start()
    {
        menu = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape") && isOpen == false)
        {
            isOpen = true;
            menu.enabled = true;
            Time.timeScale = 0f;
        }
        else if (Input.GetButtonDown("Escape") && isOpen == true)
        {
            isOpen = false;
            menu.enabled = false;
            Time.timeScale = 1f;
        }
        if(isOpen) 
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("pressed r");
                ResetButtonClicked();
            }
        }
    }
    public void ResetButtonClicked()
    {
        player.ResetToStart();
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void BackToMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
