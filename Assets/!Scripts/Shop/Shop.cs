using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnPlayerCarIDRequestMessage
{ }

public class Shop : MonoBehaviour
{
    private List<PlayerCarShopView> _carPrefabs = new List<PlayerCarShopView>();

    private PlayerAccount _playerAccount;
    private PlayerAccountConfig _playerAccountConfig;
    private ShopCarModelsConfig _shopCarModelsConfig;

    private int _currentShopCarIndex;
    private int _currentPlayerCarIndex;

    private bool _isFirstShopEntrance = true;

    public void SwitchCar(int indexDiff)
    {
        if (_isFirstShopEntrance)
        {
            MessageBroker
                .Default
                .Publish(new OnPlayerCarIDRequestMessage());
            _currentShopCarIndex = _currentPlayerCarIndex;

            SetAssortmentFromAccount();

            _isFirstShopEntrance = false;
        }       

        _currentShopCarIndex += indexDiff;

        if (_currentShopCarIndex >= _carPrefabs.Count)
        {
            _currentShopCarIndex = 0;
        }

        if (_currentShopCarIndex < 0)
        {
            _currentShopCarIndex = _carPrefabs.Count - 1;
        }

        Debug.Log(_currentShopCarIndex);

        MessageBroker
            .Default
            .Publish(new OnShopCarViewSwitchMessage(_carPrefabs[_currentShopCarIndex]));
    }

    public void PurchaseCar()
    {
        _carPrefabs[_currentShopCarIndex] = _shopCarModelsConfig.CarsL1[_currentShopCarIndex];

        MessageBroker
            .Default
            .Publish(new OnShopCarViewSwitchMessage(_carPrefabs[_currentShopCarIndex]));

        MessageBroker
            .Default
            .Publish(new OnBalanceDiffMessage(-_shopCarModelsConfig.CarsL0[_currentShopCarIndex].GetCarPurchasePrice()));
    }

    public void Launch(ShopCarModelsConfig shopCarModelConfig, PlayerAccountConfig playerAccountConfig, PlayerAccount playerAccount)
    {
        _shopCarModelsConfig = shopCarModelConfig;
        _playerAccountConfig = playerAccountConfig;        
        _playerAccount = playerAccount;

        MessageBroker
            .Default
            .Receive<OnDeclareCarIDMessage>()
            .Subscribe(message =>
            {
                _currentPlayerCarIndex = message.CarID;
            });
    }

    public string GetCurrentCarName()
    {
        return _carPrefabs[_currentShopCarIndex].GetCarModel().ToString();
    }

    public bool GetCurrentCarPurchaseStatus()
    {
        if (_carPrefabs[_currentShopCarIndex].GetCarLevel() == 0)
        {
            return false;
        }

        return true;
    }

    public int GetCurrentCarPrice()
    {
        return _carPrefabs[_currentShopCarIndex].GetCarPurchasePrice();
    }

    public eCarModel GetCurrentCarModel()
    {
        return _carPrefabs[_currentShopCarIndex].GetCarModel();
    }

    private void SetAssortmentFromAccount()
    {
        var carsDict = _playerAccount.GetCarsDict();

        foreach (var car in carsDict)
        {
            switch (car.Value)
            {
                case eCarLevel.Locked:
                    foreach (var carModel in _shopCarModelsConfig.CarsL0)
                    {
                        if (carModel.GetCarModel() == car.Key)
                        {
                            _carPrefabs.Add(carModel);
                        }
                    }
                    break;
                case eCarLevel.Lvl1:
                    foreach (var carModel in _shopCarModelsConfig.CarsL1)
                    {
                        if (carModel.GetCarModel() == car.Key)
                        {
                            _carPrefabs.Add(carModel);
                        }
                    }
                    break;
                case eCarLevel.Lvl2:
                    foreach (var carModel in _shopCarModelsConfig.CarsL2)
                    {
                        if (carModel.GetCarModel() == car.Key)
                        {
                            _carPrefabs.Add(carModel);
                        }
                    }
                    break;
                case eCarLevel.Lvl3:
                    foreach (var carModel in _shopCarModelsConfig.CarsL3)
                    {
                        if (carModel.GetCarModel() == car.Key)
                        {
                            _carPrefabs.Add(carModel);
                        }
                    }
                    break;
            }
        }
    }
}