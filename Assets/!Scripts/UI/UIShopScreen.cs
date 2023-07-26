using UnityEngine;
using TMPro;

public class UIShopScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI carNameText;
    [SerializeField] private TextMeshProUGUI carOptionText;

    private UIController _uiController;
    private Shop _shop;

    private bool _isPurchased;
    private int _carPrice;
    public void Launch(UIController uiController, Shop shop, ShopCarModelsConfig shopConfig, PlayerAccountConfig playerAccountConfig, PlayerAccount playerAccount)
    {
        _uiController = uiController;
        _shop = shop;
        shop.Launch(shopConfig, playerAccountConfig, playerAccount);
    }
    public void ShowCarOnShopEnter()
    {
        _shop.SwitchCar(0);
        OnCarSwitchLogic();
    }

    public void ShowPreviousCar()
    {
        _shop.SwitchCar(-1);
        OnCarSwitchLogic();
    }

    public void ShowNextCar()
    {
        _shop.SwitchCar(1);
        OnCarSwitchLogic();
    }

    public void SelectCurrentCar()
    {
        if (_isPurchased)
        {
            _uiController.ExitShop();
        }
        else
        {
            _shop.PurchaseCar();
            _isPurchased = true;
            carOptionText.text = "SELECT";
        }
    }

    private void OnCarSwitchLogic()
    {
        SetCarName(_shop.GetCurrentCarName());
        _isPurchased = _shop.GetCurrentCarPurchaseStatus();        
        SetCarPrice();
        SetCarOption();
    }

    private void SetCarName(string name)
    {
        carNameText.text = name;
    }

    private void SetCarOption()
    {
        if (_isPurchased)
        {
            carOptionText.text = "SELECT";
        }
        else
        {
            carOptionText.text = $"PURCHASE {_carPrice}$";
        }
    }

    private void SetCarPrice()
    {
        _carPrice = _shop.GetCurrentCarPrice();
    }
}

