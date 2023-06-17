using System.Collections.Generic;
using UnityEngine;
using UniRx;

public readonly struct OnShopCarViewSwitchMessage
{
    public readonly PlayerActiveCar CarPrefab;

    public OnShopCarViewSwitchMessage(PlayerActiveCar carPrefab)
    {
        CarPrefab = carPrefab;
    }
}

public readonly struct OnEraseCar
{ }

public class PlayerAccount : MonoBehaviour
{
    private Game _game;

    private PlayerAccountConfig _playerAccountConfig;
    private Serializer _serializer;
    private PlayerDataStorage _playerDataStorage;

    private PlayerActiveCar _activeCar;

    private Dictionary<string, int> _storage;

    private CameraSettings _camera;

    private int _odometer;

    public int GetOdometer()
    {
        return _odometer;
    }
    private void Awake()
    {
        _game = GetComponent<Game>();
        _playerAccountConfig = GetComponent<ApplicationStartUp>().PlayerAccountConfig;
        _serializer = GetComponent<Serializer>();

        _storage = _serializer.Load();
        _odometer = _storage["odometer"];
        
        _activeCar = Instantiate(_playerAccountConfig.PlayerActiveCar, _playerAccountConfig.PACSpawnPosition, transform.rotation);
        _activeCar.Launch(_game);

        _camera = Instantiate(_playerAccountConfig.Camera);
        _camera.Launch(_activeCar.transform);

        MessageBroker
            .Default
            .Receive<OnShopCarViewSwitchMessage>()
            .Subscribe(message => SwitchShopView(message.CarPrefab));
    }

    private void OnApplicationQuit()
    {
        _odometer += _activeCar.GetCurrentRideDistance();
        _serializer.Save(_odometer);
    }

    private void SwitchShopView(PlayerActiveCar carPrefab)
    {
        MessageBroker
            .Default
            .Publish(new OnEraseCar());

        _activeCar = Instantiate(carPrefab, _playerAccountConfig.PACSpawnPosition, transform.rotation);
        _activeCar.Launch(_game);
        _camera.Launch(_activeCar.transform);
    }
}
