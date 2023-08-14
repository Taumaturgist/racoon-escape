using UnityEngine;
using UnityEngine.UI;

public class UIPaintingScreen : UIScreen
{
    [SerializeField] private Button[] colorButtons;

    private UIController _uiController;
    private ColorsConfig _colorsConfig;
    private Shop _shop;

    public void Launch(UIController uiController, ColorsConfig colorsConfig, Shop shop)
    {
        _uiController = uiController;
        _colorsConfig = colorsConfig;
        _shop = shop;
    }

    public void SetColors()
    {
        var carModel = _shop.GetCurrentCarModel();

        foreach (var item in _colorsConfig.CarColors)
        {
            if (item.CarModel == carModel)
            {
                for (int i = 0; i < item.ColorChoice.Length; i++)
                {
                    colorButtons[i].gameObject.SetActive(true);
                    colorButtons[i].GetComponent<Image>().color = item.ColorChoice[i].CarColor;
                }
            }
        }
    }

    public void ExitColorsShop()
    {
        _uiController.ExitColorsShop();

        for (int i = 0; i < colorButtons.Length;i++)
        {
            colorButtons[i].gameObject.SetActive(false);
        }
    }
}
