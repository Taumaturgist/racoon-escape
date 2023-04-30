using UnityEngine;

public class DebugSettings : MonoBehaviour
{
    private DebugConfig _debugConfig;

    private void Awake()
    {
        _debugConfig = GetComponent<ApplicationStartUp>().DebugConfig;
    }
}
