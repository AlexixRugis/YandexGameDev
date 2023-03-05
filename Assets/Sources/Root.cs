using Model;
using UnityEngine;

public class Root : MonoBehaviour
{
    private const string PlayerPrefsRecordName = "Coins";

    [SerializeField] private WalletPresenter _walletPresenter;

    private Wallet _wallet;

    private void Awake()
    {
        _wallet = new Wallet(PlayerPrefs.GetInt(PlayerPrefsRecordName, 0));

        _walletPresenter.Init(_wallet);
    }
    private void OnEnable()
    {
        _wallet.CoinsAmountChanged += OnCoinsAmountChanged;
    }

    private void OnDisable()
    {
        _wallet.CoinsAmountChanged -= OnCoinsAmountChanged;    
    }

    private void OnCoinsAmountChanged()
    {
        PlayerPrefs.SetInt(PlayerPrefsRecordName, _wallet.CoinsAmount);
    }
}
