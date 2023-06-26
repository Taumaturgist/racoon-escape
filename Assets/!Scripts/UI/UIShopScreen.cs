using UnityEngine;

public class UIShopScreen : UIScreen
{
    [SerializeField] private Shop shop;
    public void Launch()
    {
        shop.Launch();
    }
}

