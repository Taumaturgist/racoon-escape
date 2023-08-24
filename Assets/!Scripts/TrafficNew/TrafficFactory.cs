using Traffic;
using UnityEngine;

public class TrafficFactory : ITrafficFactory
{
    private readonly TrafficView _trafficView;
    private readonly TrafficModel[] _trafficModel;
    private const string Traffic = "Traffic";
    private TrafficConfig _trafficConfig;

    public TrafficFactory(TrafficConfig trafficConfig)
    {
        _trafficConfig = trafficConfig;
        
        _trafficModel = trafficConfig.TrafficModel;
    }

    public TrafficView Create(TrafficModel trafficModel ,TrafficView trafficView, Vector3 position)
    {
        var randIndex = Random.Range(0, _trafficConfig.TrafficModel.Length);
        
        trafficView = GameObject.Instantiate(_trafficModel[randIndex].TrafficCar, position, Quaternion.identity);

        return trafficView;
    }
}