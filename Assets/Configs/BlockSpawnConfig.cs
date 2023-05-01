using UnityEngine;

[CreateAssetMenu(fileName = "BlockSpawnConfig", menuName = "Configs/BlockSpawnConfig", order = 51)]
public class BlockSpawnConfig : ScriptableObject
{
    [Header("Blocks")]
    public BlockSpawner BlockSpawner;
    public Block Block;

    public float OffsetZ;

    [Header("Tiles")]
    public GameObject[] CityTiles;
    public GameObject[] ForestTiles;
    public GameObject[] DesertTiles;
    public GameObject[] HighwayTiles;
    public GameObject[] TransitionTiles;

    [Header("First Block Parameters")]
    public Vector3 SpawnPointFirstBlock;
    public int TilesCountInFirstBlock;

    [Header("Other Blocks Parameters")]
    public int MinTilesCountInBlock;
    public int MaxTilesCountInBlock;
}
