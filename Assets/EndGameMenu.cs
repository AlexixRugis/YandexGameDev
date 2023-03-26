using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetScore(int score, int topScore)
    {
        _scoreText.text = $"—чЄт: {score} –екорд: {topScore}";
    }
}
