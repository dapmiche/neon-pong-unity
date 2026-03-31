using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _winScore = 10;
    int _scorePlayer1 = 0;
    int _scoreAI = 0;
    string _winner;
    Ball _ball;      
    UIManager _uiManager;
    GameState _currentState;
    public GameState CurrentState => _currentState;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _scoreSound;

    

    void Start()
    {
        _ball = FindAnyObjectByType<Ball>();
        _uiManager = FindAnyObjectByType<UIManager>();
        _currentState = GameState.Playing;
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public void AddPlayerScore()
    {    

        _scorePlayer1++;
        _audioSource.PlayOneShot(_scoreSound);
        _uiManager.UpdateScore(_scorePlayer1, _scoreAI);
        if (_scorePlayer1 >= _winScore)
        {
            _winner = "Player";
            GameOver();
            return;
        }
        _ball.ResetBall(1);             
    }

    public void AddAIScore()
    {        
        _scoreAI++;
        _audioSource.PlayOneShot(_scoreSound);
        _uiManager.UpdateScore(_scorePlayer1, _scoreAI);
        if (_scoreAI >= _winScore)
        {
            _winner = "CPU";
            GameOver();
            return;
        }
        _ball.ResetBall(-1);        
    }

    public void TogglePause()
    {
        if (_currentState == GameState.GameOver)    
            return;
        if (_currentState == GameState.Playing)
        {
            PauseGame();
        }
        else if (_currentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        _currentState = GameState.Playing;
        Time.timeScale = 1f;
        _uiManager.HidePauseMenu();
    }

    private void PauseGame()
    {
        _currentState = GameState.Paused;
        Time.timeScale = 0f;
        _uiManager.ShowPauseMenu();
    }

    void GameOver()
    {
        _currentState = GameState.GameOver;
        _uiManager.ShowGameOver(_winner);
        _ball.StopBall();
    }


}
