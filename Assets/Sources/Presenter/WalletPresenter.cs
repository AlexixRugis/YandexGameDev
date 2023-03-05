using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Model;
using System;

public class WalletPresenter : MonoBehaviour
{
    private const string AnimatorParameterName = "OnAmountChanged";

    [SerializeField] private TextMeshProUGUI _render;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _discardButton;
    [SerializeField] private TMP_InputField _amountInput;
    [SerializeField] private Animator _animator;

    private Wallet _model;

    public void Init(Wallet model)
    {
        _model = model;

        OnCoinsAmountChanged();
    }

    public void OnAddButtonClicked()
    {
        try
        {
            int amount = Convert.ToInt32(_amountInput.text);
            _model.AddCoins(amount);
        }
        catch (FormatException) { }
        catch (OverflowException) { }
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

    private void OnEnable()
    {
        _model.CoinsAmountChanged += OnCoinsAmountChanged;
        _addButton.onClick.AddListener(OnAddButtonClicked);
        _discardButton.onClick.AddListener(OnDiscardButtonClicked);
    }

    private void OnDisable()
    {
        _model.CoinsAmountChanged -= OnCoinsAmountChanged;
        _addButton.onClick.RemoveListener(OnAddButtonClicked);
        _discardButton.onClick.RemoveListener(OnDiscardButtonClicked);
    }

    private void OnCoinsAmountChanged()
    {
        _render.text = $"Coins amount: {_model.CoinsAmount}";
        _animator.SetTrigger(AnimatorParameterName);
    }
}
