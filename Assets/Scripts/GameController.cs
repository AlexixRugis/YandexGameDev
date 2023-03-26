using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private const string TopScorePlayerPrefs = "TopScore";
    private const float GameScoreMultiplier = 5f;

    [SerializeField] private WorldGenerator _worldGenerator;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private Player _player;
    [SerializeField] private float _initialGameSpeed;
    [SerializeField] private float _gameSpeedAcceleration;

    private float _gameSpeed;
    private float _gameScore;
    private float _topScore;
    private bool _gamePaused;
    private bool _playerDied;

    private void Start()
    {
        LoadTopScore();
        StartCoroutine(GameLoop());
    }



    private void OnEnable()
    {
        _player.OnDied += HandlePlayerDeath;
        _gameUI.OnGamePaused += HandleGamePause;
        _gameUI.OnGameUnpaused += HandleGameUnpause;
        _gameUI.OnGameRestarted += RestartGame;
    }

    private void OnDisable()
    {
        _player.OnDied -= HandlePlayerDeath;
        _gameUI.OnGamePaused -= HandleGamePause;
        _gameUI.OnGameUnpaused -= HandleGameUnpause;
        _gameUI.OnGameRestarted -= RestartGame;
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitUntil(() => Input.GetButton("Jump"));
        _gameSpeed = _initialGameSpeed;

        while (true)
        {
            if (_gamePaused) yield return new WaitUntil(() => !_gamePaused);

            if (_playerDied)
            {
                break;
            }


            _worldGenerator.GameSpeedMultiplier = _gameSpeed;
            _gameSpeed += _gameSpeedAcceleration * Time.deltaTime;
            _gameScore += _gameSpeed * GameScoreMultiplier * Time.deltaTime;
            _gameUI.UpdateScore(Mathf.FloorToInt(_gameScore));

            yield return null;
        }

        _worldGenerator.GameSpeedMultiplier = 0;

        if (_gameScore > _topScore)
        {
            _topScore = _gameScore;
            SaveTopScore();
        }

        _audio.PlayOneShot(_gameOverClip);
        _gameUI.ActivateEndGameMenu(Mathf.FloorToInt(_gameScore), Mathf.FloorToInt(_topScore));
    }

    private void LoadTopScore()
    {
        _topScore = PlayerPrefs.GetFloat(TopScorePlayerPrefs, 0);
    }

    private void SaveTopScore()
    {
        PlayerPrefs.SetFloat(TopScorePlayerPrefs, _topScore);
        PlayerPrefs.Save();
    }

    private void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleGameUnpause()
    {
        _gamePaused = false;
        _worldGenerator.GameSpeedMultiplier = _gameSpeed;
        _player.Unpause();
        Time.timeScale = 1;
    }

    private void HandleGamePause()
    {
        _gamePaused = true;
        _worldGenerator.GameSpeedMultiplier = 0;
        _player.Pause();
        Time.timeScale = 0;
    }

    private void HandlePlayerDeath()
    {
        _playerDied = true;
    }
}
