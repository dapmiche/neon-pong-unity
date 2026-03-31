using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text _playerScoreText;
    [SerializeField] TMP_Text _aiScoreText;
    [SerializeField] TMP_Text _winnerText;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _pauseFirstButton;
    [SerializeField] GameObject _gameOverFirstButton;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clickSound;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _gameOverPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _gameManager.TogglePause();
        }
    }

    public void UpdateScore(int playerScore, int aiScore)
    {
        
        _playerScoreText.text = $"Player: {playerScore}";
        _aiScoreText.text = $"CPU: {aiScore}";
    }

    public void ShowGameOver(string winner)
    {
        _gameOverPanel.SetActive(true);
        _winnerText.text = $"{winner} Wins!";

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_gameOverFirstButton);
    }

    public void RestartGame()
    {
        StartCoroutine(RestartCoroutine());
    }

     IEnumerator RestartCoroutine()
    {
        Time.timeScale = 1f; // Resume the game
        yield return new WaitForSecondsRealtime(_clickSound.length); // Wait for the click sound to finish
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(MainMenuCoroutine());
    }

    IEnumerator MainMenuCoroutine()
    {
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(_clickSound.length);
        SceneManager.LoadScene(0);
    }

    internal void ShowPauseMenu()
    {
        _pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_pauseFirstButton);
    }

    internal void HidePauseMenu()
    {
        _pausePanel.SetActive(false);
    }

    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
