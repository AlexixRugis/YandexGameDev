using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _render;

    public void Sync()
    {
        _render.text = _inputField.text;
    }

}
