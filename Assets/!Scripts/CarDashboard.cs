using UnityEngine;
using TMPro;

public class CarDashboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carSpeedInfo;
    [SerializeField] TextMeshProUGUI carCurrentRideDistance;
    [SerializeField] TextMeshProUGUI carOdometer;
    [SerializeField] TextMeshProUGUI carNitroCapacity;

    private PlayerActiveCar _activeCarInfo;
    private PlayerAccount _playerAccount;

    private void Awake()
    {
        _activeCarInfo = FindFirstObjectByType<PlayerActiveCar>();
        _playerAccount = FindFirstObjectByType<PlayerAccount>();
    }

    private void Update()
    {
        carSpeedInfo.text = $"Speed Km/H: {_activeCarInfo.GetSpeed()}";
        carCurrentRideDistance.text = $"Current Ride Distance, m: {_activeCarInfo.GetCurrentRideDistance()}";
        carOdometer.text = $"Odometer, m: {_playerAccount.GetOdometer()}";
        carNitroCapacity.text = $"NITRO: {_activeCarInfo?.GetNitroCapacity()}";
    }
}
