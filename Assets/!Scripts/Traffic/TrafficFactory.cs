using UnityEngine;

namespace Traffic
{
    public class TrafficFactory : ITrafficFactory
    {
        //private readonly TrafficView _trafficView;
        //private readonly TrafficModel[] _trafficModel;
        //private TrafficConfig _trafficConfig;
    
        //private GameObject _trafficPrefab;

        //public TrafficFactory(TrafficConfig trafficConfig)
        //{
        //    _trafficConfig = trafficConfig;
        //    _trafficModel = trafficConfig.TrafficModel;

        //    _trafficPrefab = new GameObject("TrafficPrefab");
        //}

        //public TrafficView Create(TrafficConfig trafficModel ,TrafficView trafficView, Vector3 position)
        //{
        //    var randIndex = Random.Range(0, _trafficConfig.TrafficModel.Length);
        
        //    trafficView = Object.Instantiate(_trafficModel[randIndex].TrafficCar, position, Quaternion.identity, _trafficPrefab.transform);

        //    return trafficView;
        //}
    }
}