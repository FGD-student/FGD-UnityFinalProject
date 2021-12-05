using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject EndingPanel;

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
