using UnityEngine;
using UniRx;

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
    }

    public void ExitShop()
    {
        shopScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);
    }

    public void ExitLoseScreen()
    {
        loseScreen.gameObject.SetActive(false);
        mainMenuScreen.gameObject.SetActive(true);
    }

    private void Awake()
    {
        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message =>
            {
                ShowLoseScreen();
            });
    }

    private void ShowLoseScreen()
    {
        actionScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
    }
}
