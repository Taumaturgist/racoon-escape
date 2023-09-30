using System.Collections.Generic;

namespace Traffic
{
    public class DesertCarFactory : TrafficCarFactory
    {
        public DesertCarFactory(List<TrafficCar> prefabs) : base(prefabs)
        {
        }
    }
}