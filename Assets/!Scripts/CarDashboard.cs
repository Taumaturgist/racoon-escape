using UnityEngine;
using TMPro;

public class CarDashboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carSpeedInfo;

    private PlayerActiveCar activeCarInfo;

    private void Awake()
    {
        activeCarInfo = FindFirstObjectByType<PlayerActiveCar>();
    }

    private void Update()
    {
        carSpeedInfo.text = $"Speed Km/H: {activeCarInfo.GetSpeed()}";
    }
}
