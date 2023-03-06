using Model;
using Zenject;

public class WalletInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        IWallet wallet = new PlayerPrefsWallet(Config.PlayerPrefsRecordName);

        Container.Bind<IWallet>()
            .FromInstance(wallet)
            .AsSingle();
    }
}