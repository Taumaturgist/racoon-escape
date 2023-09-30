using System.Collections.Generic;
using Traffic;

public class CityCarFactory : TrafficCarFactory
{
    public CityCarFactory(List<TrafficCar> prefabs) : base(prefabs)
    {
    }
}
