using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Transform menu;
    [SerializeField] private Transform gameOverWindow;
    [SerializeField] private Transform nextLevelWindow;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameOverCol overCol;
    [SerializeField] private int countEnemyDie = 1;

    private int currentCountEnemyDie;
    private bool isPause = false;
    private SoundFight soundFight;

    private void Awake()
    {
        soundFight = GetComponent<SoundFight>();
    }

    private void OnEnable()
    {
        overCol.OnActiveGameOver += OnActiveGameOver_GameManager;
        gameInput.OnMenuAction += MenuActoin_CallMenu;
    }

    private void OnDisable()
    {
        overCol.OnActiveGameOver -= OnActiveGameOver_GameManager;
        gameInput.OnMenuAction -= MenuActoin_CallMenu;
    }

    private void OnActiveGameOver_GameManager(object sender, EventArgs e)
    {
        GameOver();
    }

    private void Start()
    {

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
        soundFight.OnSoundFight();
        StartCoroutine(RestartWindow());
    }

    IEnumerator RestartWindow()
    {
        yield return new WaitForSeconds(2.0f);
        gameOverWindow.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CompleteGame()
    {
        currentCountEnemyDie++;
        if(currentCountEnemyDie >= countEnemyDie)
        {
            Time.timeScale = 0f;
            nextLevelWindow.gameObject.SetActive(true);
        }
    }

    public void NexltLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
