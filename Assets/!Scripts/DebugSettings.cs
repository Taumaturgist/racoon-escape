using UnityEngine;

public class DebugSettings : MonoBehaviour
{
    private DebugConfig _debugConfig;

    private void Awake()
    {
        _debugConfig = GetComponent<ApplicationStartUp>().DebugConfig;

        Instantiate(_debugConfig.FakeRoad, transform.position, transform.rotation);
    }
}
