using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<PlayerActiveCar> carPrefabs;

    private int _currentCarIndex;

    public void ShowPreviousCar()
    {
        Debug.Log("Previous pressed");
        SwitchCar(-1);
    }

    public void ShowNextCar()
    {
        Debug.Log("Next pressed");
        SwitchCar(1);
    }

    private void SwitchCar(int indexDiff)
    {
        _currentCarIndex += indexDiff;
       

        if (_currentCarIndex >= carPrefabs.Count)
        {
            _currentCarIndex = 0;
        }

        if (_currentCarIndex < 0)
        {
            _currentCarIndex = carPrefabs.Count - 1;
        }

        Debug.Log(_currentCarIndex);

        MessageBroker
            .Default
            .Publish(new OnShopCarViewSwitchMessage(carPrefabs[_currentCarIndex]));
    }
    public void PurchaseCar()
    {

    }
}
