using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private EndGameMenu _endGameMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public event Action OnGamePaused;
    public event Action OnGameUnpaused;
    public event Action OnGameRestarted;

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void ActivateEndGameMenu(int score, int topScore)
    {
        _endGameMenu.gameObject.SetActive(true);
        _endGameMenu.SetScore(score, topScore);
    }

    public void RestartGame()
    {
        OnGameRestarted?.Invoke();
    }

    public void ActivatePauseMenu()
    {
        _pauseMenu.SetActive(true);
        OnGamePaused?.Invoke();
    }

    public void DeactivatePauseMenu()
    {
        _pauseMenu.SetActive(false);
        OnGameUnpaused?.Invoke();
    }
}
