using System;

namespace Model
{
    public interface IWallet
    {
        public int CoinsAmount { get; }

        public event Action CoinsAmountChanged;

        public void AddCoin();
        public bool TryDiscardCoins(int price);
    }
}
