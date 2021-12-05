using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private GameObject LoadLevelPanel;
    int levelsUnlocked;

    public Button[] buttons;
    public int LeveltoUnlock = 18;

    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelLoad(int LevelIndex)
    {
        SceneManager.LoadScene(LevelIndex);
        Time.timeScale = 1;
    }

    public void Levelwon()
    {
        if (PlayerPrefs.GetInt("levelsUnlocked") < LeveltoUnlock)
        {
            PlayerPrefs.SetInt("levelsUnlocked", LeveltoUnlock);
        }
    }

    public void resetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        buttons[0].interactable = true;
    }

}