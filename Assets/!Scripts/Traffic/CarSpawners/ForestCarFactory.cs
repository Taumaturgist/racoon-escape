using System.Collections.Generic;

namespace Traffic
{
    public class ForestCarFactory : TrafficCarFactory
    {
        public ForestCarFactory(List<TrafficCar> prefabs) : base(prefabs)
        {
        }
    }
}