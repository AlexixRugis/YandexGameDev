using Model;
using System;
using UnityEngine;

public class PlayerPrefsWallet : IWallet
{
    private readonly string _key;
    private readonly Wallet _wallet;

    public int CoinsAmount => _wallet.CoinsAmount;

    public event Action CoinsAmountChanged
    {
        add => _wallet.CoinsAmountChanged += value;
        remove => _wallet.CoinsAmountChanged -= value;
    }

    public PlayerPrefsWallet(string key, int defaultValue = 0)
    {
        _wallet = new Wallet(PlayerPrefs.GetInt(key, defaultValue));
        _key = key;
    }

    public void AddCoin()
    {
        _wallet.AddCoin();
        UpdateData();
    }

    public bool TryDiscardCoins(int price)
    {
        bool result = _wallet.TryDiscardCoins(price);

        if (result)
        {
            UpdateData();
        }

        return result;
    }

    private void UpdateData()
    {
        PlayerPrefs.SetInt(_key, _wallet.CoinsAmount);
    }
}
