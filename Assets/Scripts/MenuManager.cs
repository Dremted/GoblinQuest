using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Option;

    private void Awake()
    {
        ActiveMainMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void ActiveOption()
    {
        MainMenu.SetActive(false);
        Option.SetActive(true);
    }

    public void ActiveMainMenu()
    {
        MainMenu.SetActive(true);
        Option.SetActive(false);
    }
}
