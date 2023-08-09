using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;

    [HideInInspector]
    public eBlockType BlockType;

    [HideInInspector]
    public int TilesCount;

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
    }

    public void GetFirstBlockParameters()
    {
        BlockType = eBlockType.City;
        TilesCount = _blockSpawnConfig.TilesCountInFirstBlock;
    }
    public void GetBlockParameters(eBlockType blockType)
    {
        BlockType = blockType;
        TilesCount = GetTilesCount();
    }
    public eBlockType GetBlockType(eBlockType previousBlockType)
    {
        var randomIndex = (int)previousBlockType;

        while (randomIndex == (int)previousBlockType)
        {
            var enumLen = Enum.GetNames(typeof(eBlockType)).Length;
            randomIndex = UnityEngine.Random.Range(0, enumLen);
        }

        return (eBlockType)randomIndex;
    }
    public int GetTilesCount()
    {
        var minVal = _blockSpawnConfig.MinTilesCount;
        var maxVal = _blockSpawnConfig.MaxTilesCount + 1;
        var tilesCount = UnityEngine.Random.Range(minVal, maxVal);

        return tilesCount;
    }
}