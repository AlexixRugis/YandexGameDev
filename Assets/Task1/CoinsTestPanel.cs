using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsTestPanel : MonoBehaviour
{
    [SerializeField] private CoinsStorage _storage;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private TMP_InputField _amountInput;

    private void OnEnable()
    {
        _addButton.onClick.AddListener(OnAddButtonClicked);
        _discardButton.onClick.AddListener(OnDiscardButtonClicked);
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveListener(OnAddButtonClicked);
        _discardButton.onClick.RemoveListener(OnDiscardButtonClicked);
    }

    public void OnAddButtonClicked()
    {
        _storage.AddCoin();
    }

    public void OnDiscardButtonClicked()
    {
        try
        {
            int amount = Convert.ToInt32(_amountInput.text);
            _storage.TryDiscard(amount);
        }
        catch (FormatException) { }
        catch (OverflowException) { }
    }
}
