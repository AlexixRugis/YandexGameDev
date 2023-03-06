using System;

namespace Model
{
    public class Wallet : IWallet
    {
        public int CoinsAmount { get; private set; }

        public event Action CoinsAmountChanged;

        public Wallet(int coinsAmount)
        {
            CoinsAmount = coinsAmount;
        }

        public void AddCoin()
        {
            CoinsAmount++;
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
