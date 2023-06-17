using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnPlayerCarIDRequestMessage
{ }

public class Shop : MonoBehaviour
{
    [SerializeField] private List<PlayerCarShopView> carPrefabs;

    private int _currentShopCarIndex;
    private int _currentPlayerCarIndex;

    private bool _isFirstShopEntrance = true;

    public void ShowPreviousCar()
    {
        SwitchCar(-1);
    }

    public void ShowNextCar()
    {
        SwitchCar(1);
    }

    private void SwitchCar(int indexDiff)
    {
        if (_isFirstShopEntrance)
        {
            MessageBroker
                .Default
                .Publish(new OnPlayerCarIDRequestMessage());
            _currentShopCarIndex = _currentPlayerCarIndex;
            _isFirstShopEntrance = false;
        }       

        _currentShopCarIndex += indexDiff;       

        if (_currentShopCarIndex >= carPrefabs.Count)
        {
            _currentShopCarIndex = 0;
        }

        if (_currentShopCarIndex < 0)
        {
            _currentShopCarIndex = carPrefabs.Count - 1;
        }

        Debug.Log(_currentShopCarIndex);

        MessageBroker
            .Default
            .Publish(new OnShopCarViewSwitchMessage(carPrefabs[_currentShopCarIndex]));
    }

    public void PurchaseCar()
    {

    }

    private void Start()
    {
        MessageBroker
            .Default
            .Receive<OnDeclareCarIDMessage>()
            .Subscribe(message =>
            {
                _currentPlayerCarIndex = message.CarID;
            });
    }
}
