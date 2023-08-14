using UnityEngine;
using UnityEngine.UI;

public class UIPaintingScreen : UIScreen
{
    [SerializeField] private Button[] colorButtons;

    private UIController _uiController;
    private ColorsConfig _colorsConfig;

    public void Launch(UIController uiController, ColorsConfig colorsConfig)
    {
        _uiController = uiController;
        _colorsConfig = colorsConfig;
    }

    public void SetColors(eCarModel carModel)
    {

    }

    public void ExitColorsShop()
    {
        _uiController.ExitColorsShop();
    }
}
