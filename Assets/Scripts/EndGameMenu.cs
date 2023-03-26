using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void SetScore(int score, int topScore)
    {
        if (score == topScore)
        {
            _title.text = "Победа! Новый рекорд!";
        }
        else
        {
            _title.text = "Вы проиграли";
        }
        _scoreText.text = $"Счёт: {score} Рекорд: {topScore}";
    }
}
