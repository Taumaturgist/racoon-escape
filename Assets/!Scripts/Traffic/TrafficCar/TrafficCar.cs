using UnityEngine;

namespace Traffic
{
    public class TrafficCar : MonoBehaviour
    {
        private LayerMask _layerMask;
        private TrafficCarMovement _carMovement;

        public void Launch()
        {
            
        }
        private void Start()
        {
            _carMovement = GetComponent<TrafficCarMovement>();
            
            _layerMask = LayerMask.GetMask(new[] { "Player" });
        }

       
    }
}