namespace Traffic
{
    public class TrafficController
    {
        public TrafficController(TrafficConfig trafficConfig, eBlockType blockType, Tile tile)
        {
            switch(blockType)
            {
                case eBlockType.City:
                    new CityCarSpawner(trafficConfig, tile);
                    break;
                case eBlockType.Desert:
                    new DesertCarSpawner(trafficConfig, tile);
                    break;
                case eBlockType.Forest:
                    new ForestCarSpawner(trafficConfig, tile);
                    break;
                case eBlockType.Highway:
                    new HighwayCarSpawner(trafficConfig, tile);
                    break;
            }
        }
    }
}