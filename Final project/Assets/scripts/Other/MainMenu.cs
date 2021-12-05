using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ControlPanel, mainMenuPanel, LoadLevelPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("level 1");
        Time.timeScale = 1;
        //isPaused = isPaused = true ? false : true;
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void ShowLevels()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        ControlPanel.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
