using UnityEngine;

public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] private bool isDevelopMode;

    [Header("Configs")]
    [SerializeField] private PlayerAccountConfig playerAccountConfig;

    private PlayerAccount _playerAccount;

    private void Awake()
    {
        _playerAccount = new PlayerAccount(playerAccountConfig);
    }

}
