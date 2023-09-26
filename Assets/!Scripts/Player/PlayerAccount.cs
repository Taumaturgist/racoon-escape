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

public readonly struct OnCarsAssortmentLoadedMessage
{
    public readonly CarsAssortment CarsAssortment;

    public OnCarsAssortmentLoadedMessage(CarsAssortment carsAssortment)
    {
        CarsAssortment = carsAssortment;
    }
}

public class PlayerAccount : MonoBehaviour
{
    private Game _game;

    private PlayerAccountConfig _playerAccountConfig;
    private Serializer _serializer;
    private PlayerMoney _money;

    private Dictionary<eCarModel, eCarLevel> _carsAssortmentDict = new Dictionary<eCarModel, eCarLevel>();

    private PlayerActiveCar _activeCar;

    private CameraSettings _camera;

    private int _odometer;
    private int _currentRideDistance;
    private int _balance;

    public int GetOdometer()
    {
        return _odometer;
    }

    public Dictionary<eCarModel, eCarLevel> GetCarsDict()
    {
        return _carsAssortmentDict;
    }

    private void Awake()
    {
        _game = GetComponent<Game>();
        
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
            .Subscribe(message =>
            {
                SwitchShopView(message.CarPrefab.GetComponent<PlayerActiveCar>());
                _carsAssortmentDict[message.CarPrefab.GetCarModel()] = message.CarPrefab.GetCarLevel();
            });

        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message =>
            {
                _activeCar.FreezeRotation(false);
                RestoreActiveCarDefaults();
            });

        MessageBroker
            .Default
            .Receive<OnLoseScreenExitMessage>()
            .Subscribe(message =>
            {
                _activeCar.FreezeRotation(true);
                RestoreActiveCarDefaults();
            });

        MessageBroker
            .Default
            .Receive<OnBalanceDiffMessage>()
            .Subscribe(message =>
            {
                ProcessBalance(message.Diff);
                Debug.Log($"Balance change: {message.Diff}");
            });

        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message => UpdateOdometer());
    }

    private void Start()
    {
        LoadPlayerData();
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

        if (playerData.CarsAssortmentDict.Count == 0)
        {
            InitDefaultCarDictionary();
            Debug.Log("Default cars loaded");
        }
        else
        {
            _carsAssortmentDict = playerData.CarsAssortmentDict;
        }
        
        _odometer = playerData.Odometer;
        _balance = playerData.Balance;

        if (_playerAccountConfig.IsCheatMode)
        { 
            _balance = _playerAccountConfig.CheatBalance;
        }

        MessageBroker
            .Default
            .Publish(new OnDataLoadedMessage(playerData));
    }

    private void SavePlayerData()
    {      
        _serializer.Save(new PlayerData(_carsAssortmentDict, _odometer, _balance));
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
        Debug.Log(_balance);
    }

    private void UpdateOdometer()
    {
        _currentRideDistance = _activeCar.GetCurrentRideDistance();
        _odometer += _currentRideDistance;

        MessageBroker
            .Default
            .Publish(new OnOdometerUpdateMessage(_currentRideDistance, _odometer));
    }    

    private void InitDefaultCarDictionary()
    {
        _carsAssortmentDict.Add(eCarModel.ChevroletCamaroSS1969, eCarLevel.Lvl1);
        _carsAssortmentDict.Add(eCarModel.ToyotaTundra, eCarLevel.Locked);
        _carsAssortmentDict.Add(eCarModel.NissanSkylineGT, eCarLevel.Locked);
        _carsAssortmentDict.Add(eCarModel.DodgeViperGTS, eCarLevel.Locked);
        _carsAssortmentDict.Add(eCarModel.MercedesBenzGCLass, eCarLevel.Locked);
        _carsAssortmentDict.Add(eCarModel.LamborghiniHuracanLP700, eCarLevel.Locked);
    }

    private void RestoreActiveCarDefaults()
    {        
        _activeCar.transform.position = _playerAccountConfig.PACSpawnPosition;
        _activeCar.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }
}