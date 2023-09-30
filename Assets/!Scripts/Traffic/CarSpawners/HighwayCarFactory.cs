using System.Collections.Generic;

namespace Traffic
{
    public class HighwayCarFactory : TrafficCarFactory
    {
        public HighwayCarFactory(List<TrafficCar> prefabs) : base(prefabs)
        {
        }
    }
}