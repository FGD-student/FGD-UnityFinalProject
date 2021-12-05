using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int coins;
    public Animator transition;
    public float transitionTime = 1f;

    public bool isPaused = false;
    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private static GameManager instance;

    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    public void SwitchPause()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        isPaused = isPaused = true ? false : true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void Finish()
    {
        //LoadNextLevel();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
    }

    public void Death()
    {
        LoadSameLevel();
    }

    public void LoadSameLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 0));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
    
}
