using UnityEngine;
using UniRx;

public readonly struct OnLoseScreenExitMessage
{ }
public class UIController : MonoBehaviour
{
    [SerializeField] UIMainMenu mainMenuScreen;
    [SerializeField] UIActionScreen actionScreen;
    [SerializeField] UIHUD hud;
    [SerializeField] UISettingsScreen settingsScreen;
    [SerializeField] UIShopScreen shopScreen;
    [SerializeField] UITuningScreen tuningScreen;
    [SerializeField] UIPaintingScreen paintingScreen;
    [SerializeField] UILoseScreen loseScreen;
 
    public void StartGame()
    {
        MessageBroker
            .Default
            .Publish(new OnGameStartMessage());

        mainMenuScreen.gameObject.SetActive(false);
        actionScreen.gameObject.SetActive(true);
    }

    public void EnterShop()
    {
        mainMenuScreen.gameObject.SetActive(false);
        shopScreen.gameObject.SetActive(true);
        shopScreen.ShowCarOnShopEnter();

        MessageBroker
            .Default
            .Publish(new OnShopEnterCamSwitchMessage(true));
    }

    public void ExitShop()
    {
        shopScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);

        MessageBroker
            .Default
            .Publish(new OnShopEnterCamSwitchMessage(false));
    }

    public void EnterColorsShop()
    {
        shopScreen.gameObject.SetActive(false);
        paintingScreen.gameObject.SetActive(true);
    }

    public void ExitColorsShop()
    {
        paintingScreen.gameObject.SetActive(false);
        shopScreen.gameObject.SetActive(true);
    }

    public void ExitLoseScreen()
    {
        loseScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);

        MessageBroker
            .Default
            .Publish(new OnLoseScreenExitMessage());
    }

    public void Launch(
        Shop shop, 
        ShopCarModelsConfig shopConfig, 
        PlayerAccountConfig playerAccountConfig, 
        PlayerAccount playerAccount, 
        ColorsConfig colorsConfig)
    {
        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message =>
            {
                ShowLoseScreen();
            });

        hud.Launch();
        shopScreen.Launch(this, shop, shopConfig, playerAccountConfig, playerAccount);
        loseScreen.Launch();
        paintingScreen.Launch(this, colorsConfig);
    }

    private void ShowLoseScreen()
    {
        actionScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
    }
}
