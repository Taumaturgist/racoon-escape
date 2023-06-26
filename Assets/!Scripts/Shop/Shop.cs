using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnPlayerCarIDRequestMessage
{ }

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopCarModelsConfig shopCarModelsConfig;
    [SerializeField] private PlayerAccountConfig playerAccountConfig;

    [SerializeField] private List<PlayerCarShopView> carPrefabs;

    [SerializeField] private List<CarPool> carPools;  

    private List<PlayerCarShopView> _carViewsL0;
    private List<PlayerCarShopView> _carViewsL1;
    private List<PlayerCarShopView> _carViewsL2;
    private List<PlayerCarShopView> _carViewsL3;

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

    public void Launch()
    {
        InstantiateCarModelsPool();

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
        for (int i = 0; i < shopCarModelsConfig.carsL0.Count; i++)
        {
            var go = Instantiate(shopCarModelsConfig.carsL0[i], playerAccountConfig.PACSpawnPosition, shopCarModelsConfig.carsL0[i].transform.rotation, carPools[0].transform);
            _carViewsL0.Add(go);
            Debug.Log($"{go} added to pool");
            go.gameObject.SetActive(false);            
        }

        for (int i = 0; i < shopCarModelsConfig.carsL1.Count; i++)
        {
            var go = Instantiate(shopCarModelsConfig.carsL1[i], playerAccountConfig.PACSpawnPosition, shopCarModelsConfig.carsL1[i].transform.rotation, carPools[1].transform);
            _carViewsL1.Add(go);
            go.gameObject.SetActive(false);
        }

        for (int i = 0; i < shopCarModelsConfig.carsL2.Count; i++)
        {
            var go = Instantiate(shopCarModelsConfig.carsL2[i], playerAccountConfig.PACSpawnPosition, shopCarModelsConfig.carsL2[i].transform.rotation, carPools[2].transform);
            _carViewsL2.Add(go);
            go.gameObject.SetActive(false);
        }

        for (int i = 0; i < shopCarModelsConfig.carsL3.Count; i++)
        {
            var go = Instantiate(shopCarModelsConfig.carsL3[i], playerAccountConfig.PACSpawnPosition, shopCarModelsConfig.carsL3[i].transform.rotation, carPools[3].transform);
            _carViewsL3.Add(go);
            go.gameObject.SetActive(false);
        }
    }
}
