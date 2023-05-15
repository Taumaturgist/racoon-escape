using UnityEngine;

[CreateAssetMenu(fileName = "BlockSpawnConfig", menuName = "Configs/BlockSpawnConfig", order = 51)]
public class BlockSpawnConfig : ScriptableObject
{
    [Header("Blocks")]
    public BlockSpawner BlockStorage;
    public Block Block;

    public int OffsetZ;
    public int CrossroadNumberInCity;

    [Header("Tiles")]
    public GameObject[] CityTiles;
    public GameObject[] DesertTiles;
    public GameObject[] ForestTiles;
    public GameObject[] HighwayTiles;

    [Header("Transition Tiles")]
    public GameObject CityDesertTile;
    public GameObject CityForestTile;
    public GameObject CityHighwayTile;

    public GameObject DesertCityTile;
    public GameObject DesertForestTile;
    public GameObject DesertHighwayTile;

    public GameObject ForestCityTile;
    public GameObject ForestDesertTile;
    public GameObject ForestHighwayTile;

    public GameObject HighwayCityTile;
    public GameObject HighwayDesertTile;
    public GameObject HighwayForestTile;

    [Header("First Block Parameters")]
    public Vector3 SpawnPointFirstBlock;
    public int TilesCountInFirstBlock;

    [Header("Other Blocks Parameters")]
    public int MinTilesCount;
    public int MaxTilesCount;
}
