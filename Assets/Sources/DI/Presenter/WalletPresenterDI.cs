using Model;
using TMPro;
using UnityEngine;
using Zenject;

public class WalletPresenterDI : MonoBehaviour
{
    private const string AnimatorParameterName = "OnAmountChanged";

    [SerializeField] private TextMeshProUGUI _render;
    [SerializeField] private Animator _animator;

    private IWallet _model;

    private void OnEnable()
    {
        _model.CoinsAmountChanged += OnCoinsAmountChanged;
    }

    private void OnDisable()
    {
        _model.CoinsAmountChanged -= OnCoinsAmountChanged;
    }

    [Inject]
    public void Init(IWallet model)
    {
        _model = model;

        OnCoinsAmountChanged();
    }

    private void OnCoinsAmountChanged()
    {
        _render.text = $"Coins amount: {_model.CoinsAmount}";
        _animator.SetTrigger(AnimatorParameterName);
    }
}
