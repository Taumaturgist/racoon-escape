using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnPlayerCarIDRequestMessage
{ }

public class Shop : MonoBehaviour    
{
    [SerializeField] private List<CarPool> carPools;

    [SerializeField] private List<PlayerCarShopView> carViewsL0;
    [SerializeField] private List<PlayerCarShopView> carViewsL1;
    [SerializeField] private List<PlayerCarShopView> carViewsL2;
    [SerializeField] private List<PlayerCarShopView> carViewsL3;

    private List<PlayerCarShopView> _carPrefabs;    

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
        _carPrefabs[_currentShopCarIndex] = _shopCarModelsConfig.carsL1[_currentShopCarIndex];

        MessageBroker
            .Default
            .Publish(new OnShopCarViewSwitchMessage(_carPrefabs[_currentShopCarIndex]));

        MessageBroker
            .Default
            .Publish(new OnBalanceDiffMessage(-_shopCarModelsConfig.carsL0[_currentShopCarIndex].GetCarPurchasePrice()));

    }

    public void Launch(ShopCarModelsConfig shopCarModelConfig, PlayerAccountConfig playerAccountConfig)
    {
        _shopCarModelsConfig = shopCarModelConfig;
        _playerAccountConfig = playerAccountConfig;        

        _carPrefabs = _shopCarModelsConfig.carsL0;

        //InstantiateCarModelsPool();

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
        return _carPrefabs[_currentShopCarIndex].GetCarModelName();    
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

    //return to this later in need of optimisation
    //private void InstantiateCarModelsPool()
    //{
    //    for (int i = 0; i < _shopCarModelsConfig.carsL0.Count; i++)
    //    {
    //        var go = Instantiate(_shopCarModelsConfig.carsL0[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL0[i].transform.rotation, carPools[0].transform);
    //        carViewsL0.Add(go);
    //        Debug.Log($"{go} added to pool");
    //        go.gameObject.SetActive(false);            
    //    }

    //    for (int i = 0; i < _shopCarModelsConfig.carsL1.Count; i++)
    //    {
    //        var go = Instantiate(_shopCarModelsConfig.carsL1[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL1[i].transform.rotation, carPools[1].transform);
    //        carViewsL1.Add(go);
    //        go.gameObject.SetActive(false);
    //    }

    //    for (int i = 0; i < _shopCarModelsConfig.carsL2.Count; i++)
    //    {
    //        var go = Instantiate(_shopCarModelsConfig.carsL2[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL2[i].transform.rotation, carPools[2].transform);
    //        carViewsL2.Add(go);
    //        go.gameObject.SetActive(false);
    //    }

    //    for (int i = 0; i < _shopCarModelsConfig.carsL3.Count; i++)
    //    {
    //        var go = Instantiate(_shopCarModelsConfig.carsL3[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL3[i].transform.rotation, carPools[3].transform);
    //        carViewsL3.Add(go);
    //        go.gameObject.SetActive(false);
    //    }
    //}
}
