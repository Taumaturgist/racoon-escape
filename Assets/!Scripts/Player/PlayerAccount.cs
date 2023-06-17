using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnApplicationQuit()
    {
        _odometer += _activeCar.GetCurrentRideDistance();
        _serializer.Save(_odometer);
    }
}
