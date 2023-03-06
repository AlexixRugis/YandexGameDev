using UnityEngine;
using TMPro;

public class CoinsView : MonoBehaviour
{
    [SerializeField] private CoinsStorage _storage;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _render;

    private void Start()
    {
        OnCoinsAmountChanged();
    }

    private void OnEnable()
    {
        _storage.CoinsAmountChanged += OnCoinsAmountChanged;
    }

    private void OnDisable()
    {
        _storage.CoinsAmountChanged -= OnCoinsAmountChanged;
    }

    private void OnCoinsAmountChanged()
    {
        _render.text = $"Coins: {_storage.Amount}";
        _animator.SetTrigger("OnPickupCoin");
    }
}
