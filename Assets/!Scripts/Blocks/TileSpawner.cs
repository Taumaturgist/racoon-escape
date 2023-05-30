using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private GameObject _transitionTile;

    public void Launch(BlockSpawnConfig blockSpawnConfig)
    {
        _blockSpawnConfig = blockSpawnConfig;
    }

    public void CreateTiles(Block block, ref eBlockType previousBlockType, ref eBlockType nextBlockType, ref Vector3 pos, Quaternion rot)
    {
        var tiles = new GameObject[block.TilesCount];
        var tileSet = _blockSpawnConfig.CityTiles;
        var blockType = block.BlockType;

        switch (blockType)
        {
            case eBlockType.City:
                tileSet = _blockSpawnConfig.CityTiles;
                break;
            case eBlockType.Desert:
                tileSet = _blockSpawnConfig.DesertTiles;
                break;
            case eBlockType.Forest:
                tileSet = _blockSpawnConfig.ForestTiles;
                break;
            case eBlockType.Highway:
                tileSet = _blockSpawnConfig.HighwayTiles;
                break;
        }
        var crossroadCount = 0;

        int randomIndex;
        for (int i = 0; i < tiles.Length - 1; i++)
        {
            randomIndex = Random.Range(0, tileSet.Length);
            if (crossroadCount < 2)
            {
                tiles[i] = Instantiate(tileSet[randomIndex], pos, rot, block.transform);
                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                // Crossroad index = 0
                randomIndex = Random.Range(_blockSpawnConfig.CrossroadNumberInCity, tileSet.Length);

                tiles[i] = Instantiate(tileSet[randomIndex], pos, rot, block.transform);
            }

            pos.z += _blockSpawnConfig.OffsetZ;
        }

        previousBlockType = blockType;
        nextBlockType = block.GetBlockType(previousBlockType);

        _transitionTile = GetTransitionTile(block, tiles, previousBlockType, nextBlockType, ref pos);

        pos.z += _blockSpawnConfig.OffsetZ;
    }
    private GameObject GetTransitionTile(Block block, GameObject[] tiles, eBlockType previousBlockType, eBlockType nextBlockType, ref Vector3 pos)
    {
        var lastIndex = tiles.Length - 1;
        switch (previousBlockType)
        {
            case eBlockType.City:
                switch (nextBlockType)
                {
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityDesertTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityForestTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.CityHighwayTile, block.gameObject, ref pos);
                        break;
                }
                break;
            case eBlockType.Desert:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertCityTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertForestTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.DesertHighwayTile, block.gameObject, ref pos);
                        break;
                }
                break;
            case eBlockType.Forest:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestCityTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestDesertTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.ForestHighwayTile, block.gameObject, ref pos);
                        break;
                }
                break;
            case eBlockType.Highway:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayCityTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayDesertTile, block.gameObject, ref pos);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTile(_blockSpawnConfig.HighwayForestTile, block.gameObject, ref pos);
                        break;
                }
                break;
        }

        return tiles[lastIndex];
    }
    private GameObject CreateTransitionTile(GameObject obj, GameObject parent, ref Vector3 pos)
    {
        return Instantiate(obj, pos, obj.transform.rotation, parent.transform);
    }

    public BoxCollider GetTransitionTileCollider()
    {
        return _transitionTile.GetComponent<BoxCollider>();
    }
}
