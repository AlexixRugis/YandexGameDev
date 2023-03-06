using System;
using UnityEngine;

public class CoinsStorage : MonoBehaviour
{
    public int Amount { get; private set; }

    public event Action CoinsAmountChanged;

    private void Awake()
    {
        Amount = PlayerPrefs.GetInt("Coins", 0);
    }

    public void AddCoin()
    {
        Amount++;
        UpdateCoinsData();
    }

    public bool TryDiscard(int price)
    {
        if (Amount < price)
            return false;

        Amount -= price;
        UpdateCoinsData();

        return true;
    }

    private void UpdateCoinsData()
    {
        CoinsAmountChanged?.Invoke();
        PlayerPrefs.SetInt("Coins", Amount);
    }
}
