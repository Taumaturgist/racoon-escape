using UnityEngine;
using TMPro;

public class DebugDashboard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugFPSInfo;
    void Update()
    {
        debugFPSInfo.text = $"FPS: {Mathf.RoundToInt(1.0f / Time.smoothDeltaTime)}";
    }
}