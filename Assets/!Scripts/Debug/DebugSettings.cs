using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSettings : MonoBehaviour
{
    private DebugConfig _debugConfig;

    private void Awake()
    {
        _debugConfig = GetComponent<ApplicationStartUp>().DebugConfig;
    }

    private void Update()
    {
        Restart();
    }

    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
    }
}
