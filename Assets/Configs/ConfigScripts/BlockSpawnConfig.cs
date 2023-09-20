using UnityEngine;

[CreateAssetMenu(fileName = "BlockSpawnConfig", menuName = "Configs/BlockSpawnConfig")]
public class BlockSpawnConfig : ScriptableObject
{
    public int OffsetZ;
    public int CrossroadNumberInCity;

    public BlockSpawner BlockStorage;
    public Block Block;
    public TileSpawner TileSpawner;
    public Tile Tile;

    [Header("First Block Parameters")]
    public Vector3 SpawnPointFirstBlock;
    public int TilesCountInFirstBlock;

    [Header("Other Blocks Parameters")]
    public int MinTilesCount;
    public int MaxTilesCount;

    [Header("Tiles")]
    public Tile[] CityTiles;
    public Tile[] DesertTiles;
    public Tile[] ForestTiles;
    public Tile[] HighwayTiles;

    [Header("Transition Tiles")]
    public Tile CityDesertTile;
    public Tile CityForestTile;
    public Tile CityHighwayTile;

    public Tile DesertCityTile;
    public Tile DesertForestTile;
    public Tile DesertHighwayTile;

    public Tile ForestCityTile;
    public Tile ForestDesertTile;
    public Tile ForestHighwayTile;

    public Tile HighwayCityTile;
    public Tile HighwayDesertTile;
    public Tile HighwayForestTile;
}