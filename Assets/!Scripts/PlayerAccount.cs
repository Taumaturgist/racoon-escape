using UnityEngine;

public class PlayerAccount : MonoBehaviour
{
    private PlayerAccountConfig _playerAccountConfig;

    public PlayerAccount(PlayerAccountConfig playerAccountConfig)
    {
        _playerAccountConfig = playerAccountConfig;
    }
}
