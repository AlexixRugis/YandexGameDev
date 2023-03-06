using Model;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Root : MonoBehaviour
{
    [SerializeField] private WalletPresenter _walletPresenter;
    [SerializeField] private TestPanelPresenter _testPanelPresenter;

    private IWallet _wallet;

    private void Awake()
    {
        _wallet = new PlayerPrefsWallet(Config.PlayerPrefsRecordName);

        _walletPresenter.Init(_wallet);
        _testPanelPresenter.Init(_wallet);
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
        PlayerPrefs.SetInt(Config.PlayerPrefsRecordName, _wallet.CoinsAmount);
    }
}
