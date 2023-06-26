using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnPlayerCarIDRequestMessage
{ }

public class Shop : MonoBehaviour
{
    private List<PlayerCarShopView> _carPrefabs;

    private List<CarPool> _carPools;  

    private List<PlayerCarShopView> _carViewsL0;
    private List<PlayerCarShopView> _carViewsL1;
    private List<PlayerCarShopView> _carViewsL2;
    private List<PlayerCarShopView> _carViewsL3;

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

    }

    private void Awake()
    {
        _playerAccount = GetComponent<PlayerAccount>();
        _playerAccountConfig = GetComponent<ApplicationStartUp>().PlayerAccountConfig;
        _shopCarModelsConfig = GetComponent<ApplicationStartUp>().ShopCarModelsConfig;

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

    private void InstantiateCarModelsPool()
    {
        for (int i = 0; i < _shopCarModelsConfig.carsL0.Count; i++)
        {
            var go = Instantiate(_shopCarModelsConfig.carsL0[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL0[i].transform.rotation, _carPools[0].transform);
            _carViewsL0.Add(go);
            Debug.Log($"{go} added to pool");
            go.gameObject.SetActive(false);            
        }

        for (int i = 0; i < _shopCarModelsConfig.carsL1.Count; i++)
        {
            var go = Instantiate(_shopCarModelsConfig.carsL1[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL1[i].transform.rotation, _carPools[1].transform);
            _carViewsL1.Add(go);
            go.gameObject.SetActive(false);
        }

        for (int i = 0; i < _shopCarModelsConfig.carsL2.Count; i++)
        {
            var go = Instantiate(_shopCarModelsConfig.carsL2[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL2[i].transform.rotation, _carPools[2].transform);
            _carViewsL2.Add(go);
            go.gameObject.SetActive(false);
        }

        for (int i = 0; i < _shopCarModelsConfig.carsL3.Count; i++)
        {
            var go = Instantiate(_shopCarModelsConfig.carsL3[i], _playerAccountConfig.PACSpawnPosition, _shopCarModelsConfig.carsL3[i].transform.rotation, _carPools[3].transform);
            _carViewsL3.Add(go);
            go.gameObject.SetActive(false);
        }
    }
}
