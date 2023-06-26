using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnShopCarViewSwitchMessage
{
    public readonly PlayerCarShopView CarPrefab;

    public OnShopCarViewSwitchMessage(PlayerCarShopView carPrefab)
    {
        CarPrefab = carPrefab;
    }
}

public readonly struct OnEraseCarMessage
{ }

public readonly struct OnDeclareCarIDMessage
{
    public readonly int CarID;

    public OnDeclareCarIDMessage(int carID)
    {
        CarID = carID;
    }
}

public readonly struct OnDataLoadedMessage
{
    public readonly PlayerData PlayerData;

    public OnDataLoadedMessage(PlayerData playerData)
    {
        PlayerData = playerData;
    }
}

public readonly struct OnOdometerUpdateMessage
{
    public readonly int CurrentRideDistance;
    public readonly int Odometer;

    public OnOdometerUpdateMessage(int currentRideDistance, int odometer)
    {
        CurrentRideDistance = currentRideDistance;
        Odometer = odometer;
    }
}

public class PlayerAccount : MonoBehaviour
{
    private Game _game;
    private Shop _shop;

    private PlayerAccountConfig _playerAccountConfig;
    private Serializer _serializer;
    private PlayerMoney _money;
    private PlayerDataStorage _playerDataStorage;

    private PlayerActiveCar _activeCar;   

    private CameraSettings _camera;

    private int _odometer;
    private int _currentRideDistance;
    private int _balance;

    public int GetOdometer()
    {
        return _odometer;
    }

    private void Awake()
    {
        _game = GetComponent<Game>();
        _shop = GetComponent<Shop>();
        _playerAccountConfig = GetComponent<ApplicationStartUp>().PlayerAccountConfig;
        _serializer = GetComponent<Serializer>(); 
        _money = GetComponent<PlayerMoney>();


        _camera = Instantiate(_playerAccountConfig.Camera);

        SetUpNewActiveCar(_playerAccountConfig.PlayerActiveCar);
        
        MessageBroker
            .Default
            .Receive<OnPlayerCarIDRequestMessage>()
            .Subscribe(message =>
            {
                MessageBroker
                .Default
                .Publish(new OnDeclareCarIDMessage(_activeCar.GetComponent<PlayerCarShopView>().GetCarModelID()));
            });        

        MessageBroker
            .Default
            .Receive<OnShopCarViewSwitchMessage>()
            .Subscribe(message => SwitchShopView(message.CarPrefab.GetComponent<PlayerActiveCar>()));

        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message =>
            {
                _activeCar.transform.position = _playerAccountConfig.PACSpawnPosition;
                _activeCar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            });

        MessageBroker
            .Default
            .Receive<OnRaceSalaryCountMessage>()
            .Subscribe(message =>
            {
                ProcessBalance(message.Salary);
            });

        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message => UpdateOdometer());
    }

    private void SwitchShopView(PlayerActiveCar carPrefab)
    {
        MessageBroker
            .Default
            .Publish(new OnEraseCarMessage());

        SetUpNewActiveCar(carPrefab);
    }

    private void LoadPlayerData()
    {
        var playerData = _serializer.Load();
        _odometer = playerData.Odometer;
        _balance = playerData.Balance;

        MessageBroker
            .Default
            .Publish(new OnDataLoadedMessage(playerData));
    }

    private void SavePlayerData()
    {      
        _serializer.Save(new PlayerData(_odometer, _balance));
    }

    private void SetUpNewActiveCar(PlayerActiveCar carPrefab)
    {
        _activeCar = Instantiate(carPrefab, _playerAccountConfig.PACSpawnPosition, transform.rotation);
        _activeCar.Launch(_game);
        _money.SetActiveCar(_activeCar);

        _camera.Launch(_activeCar.transform, _activeCar.GetComponent<PlayerCarShopView>().GetCarModelID());
    }

    private void ProcessBalance(int diff)
    {
        _balance += diff;
    }

    private void UpdateOdometer()
    {
        _currentRideDistance = _activeCar.GetCurrentRideDistance();
        _odometer += _currentRideDistance;

        MessageBroker
            .Default
            .Publish(new OnOdometerUpdateMessage(_currentRideDistance, _odometer));
    }

    private void Start()
    {
        LoadPlayerData();
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }    
}
