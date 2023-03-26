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
            _title.text = "������! ����� ������!";
        }
        else
        {
            _title.text = "�� ���������";
        }
        _scoreText.text = $"����: {score} ������: {topScore}";
    }
}
