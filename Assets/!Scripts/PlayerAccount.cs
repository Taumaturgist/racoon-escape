using UnityEngine;

public class PlayerAccount : MonoBehaviour
{
    private PlayerAccountConfig _playerAccountConfig;

    private void Awake()
    {
        _playerAccountConfig = GetComponent<ApplicationStartUp>().PlayerAccountConfig;

        var go = Instantiate(_playerAccountConfig.PlayerActiveCar, _playerAccountConfig.PACSpawnPosition, transform.rotation);
        go.Launch(_playerAccountConfig);
    }
}
