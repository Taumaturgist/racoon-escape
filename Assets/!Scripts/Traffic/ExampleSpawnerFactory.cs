using UnityEngine;

namespace Assets._Scripts.Traffic
{
    public class ExampleSpawnerFactory : MonoBehaviour
    {
        private ISpawnerFactory _spawnerFactory;

        private void Start()
        {
            _spawnerFactory = new CityNPCSpawnerFactory();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _spawnerFactory.SpawnNPC();
            }
        }
    }

}
