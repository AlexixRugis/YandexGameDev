using System;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class Wallet
    {
        public int CoinsAmount { get; private set; }

        public event Action CoinsAmountChanged;

        public Wallet(int coinsAmount)
        {
            CoinsAmount = coinsAmount;
        }

        public void AddCoins(int amount)
        {
            if (amount < 0) throw new InvalidOperationException("Can't add negative amount of coins");

            CoinsAmount += amount;
            CoinsAmountChanged?.Invoke();
        }

        public bool TryDiscardCoins(int price)
        {
            if (price < 0) throw new System.InvalidOperationException("Can't discard negative amount of coins");

            if (CoinsAmount < price) return false;

            CoinsAmount -= price;
            CoinsAmountChanged?.Invoke();

            return true;
        }
    }
}
