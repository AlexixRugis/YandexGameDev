using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private const string PlayerPrefsTutorial = "TutorialCompleted";

    [SerializeField] private TextMeshProUGUI _text;

    private int _isFirstPlay;

    private void Start()
    {
        _isFirstPlay = PlayerPrefs.GetInt(PlayerPrefsTutorial, 1);
        StartCoroutine(TutorialRoutine());
    }

    private IEnumerator TutorialRoutine()
    {
        _text.enabled = false;

        bool needTutorial = true;
        if (_isFirstPlay == 0)
        {
            for (float time = 0; time < 15f; time += Time.deltaTime)
            {
                if (Input.GetButton("Jump"))
                {
                    needTutorial = false;
                    break;
                }
                yield return null;
            }
        }

        if (needTutorial)
        {
            _text.enabled = true;
            yield return new WaitUntil(() => Input.GetButton("Jump"));
            _text.enabled = false;

            PlayerPrefs.SetInt(PlayerPrefsTutorial, 0);
            PlayerPrefs.Save();
        }
    }
}
