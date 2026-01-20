using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Transform menu;
    [SerializeField] private Transform GameOverWindow;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameOverCol overCol;

    private bool isPause = false;

    private void OnEnable()
    {
        overCol.OnActiveGameOver += OnActiveGameOver_GameManager;
    }

    private void OnActiveGameOver_GameManager(object sender, EventArgs e)
    {
        GameOver();
    }

    private void Start()
    {
        gameInput.OnMenuAction += MenuActoin_CallMenu;
        menu.gameObject.SetActive(false);
    }

    private void MenuActoin_CallMenu(object sender, EventArgs e)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPause = !isPause;

        if (isPause)
        {
            menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            menu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void GameOver()
    {
        StartCoroutine(RestartWindow());
    }

    IEnumerator RestartWindow()
    {
        yield return new WaitForSeconds(2.0f);
        GameOverWindow.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {

    }
}
