using UnityEngine;

public class UIShopScreen : UIScreen
{
    private Shop _shop;
    public void Launch(Shop shop)
    {
        _shop = shop;
    }

    public void ShowPreviousCar()
    {
        _shop.SwitchCar(-1);
    }

    public void ShowNextCar()
    {
        _shop.SwitchCar(1);
    }
}

