using Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestPanelPresenterDI : MonoBehaviour
{
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private TMP_InputField _amountInput;

    private IWallet _model;

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

    [Inject]
    public void Init(IWallet model)
    {
        _model = model;
    }

    public void OnAddButtonClicked()
    {
        _model.AddCoin();
    }

    public void OnDiscardButtonClicked()
    {
        try
        {
            int amount = Convert.ToInt32(_amountInput.text);
            _model.TryDiscardCoins(amount);
        }
        catch (FormatException) { }
        catch (OverflowException) { }
    }
}
