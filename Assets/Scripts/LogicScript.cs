using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseMenuUI;
    public GameObject victoryScreen;

    public static bool gameIsPaused = false;
    public static bool gameIsOver = false;


    [ContextMenu("Increase Score")]
    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !gameIsOver)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public IEnumerator GameOver()
    {
        gameIsOver = true;
        yield return new WaitForSeconds(2.5f);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public IEnumerator Victory()
    {
        gameIsOver = true;
        yield return new WaitForSeconds(2.5f);
        victoryScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsOver = false;
    }

    public void BackToMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
