using Traffic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficConfig;

    private Tile[] _tiles;
    private Tile[] _tileSet;
    private Tile _transitionTile;
    
    private eBlockType _blockType;
    private bool _isFirstTile;
    private const int MaxBlockAmountInScene = 2;

    public void Launch(
        BlockSpawnConfig blockSpawnConfig,
        BuildingSpawnConfig buildingSpawnConfig,
        TrafficConfig trafficConfig,
        Block block,
        ref eBlockType previousBlockType,
        ref eBlockType nextBlockType,
        ref Vector3 pos, Quaternion rot,
        bool isFirstBlock)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficConfig = trafficConfig;
        _blockType = block.BlockType;
        _isFirstTile = isFirstBlock;
        CreateTiles(block, ref previousBlockType, ref nextBlockType, ref pos, rot);
    }

    private void CreateTiles(Block block, ref eBlockType previousBlockType, ref eBlockType nextBlockType,
        ref Vector3 pos, Quaternion rot)
    {
        _tiles = new Tile[block.TilesCount];
        var blockType = block.BlockType;

        switch (blockType)
        {
            case eBlockType.City:
                _tileSet = _blockSpawnConfig.CityTiles;
                break;
            case eBlockType.Desert:
                _tileSet = _blockSpawnConfig.DesertTiles;
                break;
            case eBlockType.Forest:
                _tileSet = _blockSpawnConfig.ForestTiles;
                break;
            case eBlockType.Highway:
                _tileSet = _blockSpawnConfig.HighwayTiles;
                break;
        }
        var crossroadCount = 0;

        int randomIndex;
        for (int i = 0; i < _tiles.Length - 1; i++)
        {
            
            randomIndex = Random.Range(0, _tileSet.Length);
            if (_isFirstTile)
            {
                _tiles[i] = Instantiate(_tileSet[randomIndex], pos, rot, transform);
                
                _tiles[i].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);

                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
                _isFirstTile = false;
                pos.z += _blockSpawnConfig.OffsetZ;
                continue;
            }
            
            if (crossroadCount < MaxBlockAmountInScene)
            {
                _tiles[i] = Instantiate(_tileSet[randomIndex], pos, rot, transform);
                
                _tiles[i].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);

                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                // Crossroad index = 0
                randomIndex = Random.Range(_blockSpawnConfig.CrossroadNumberInCity, _tileSet.Length);

                _tiles[i] = Instantiate(_tileSet[randomIndex], pos, rot, transform);

                _tiles[i].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
            }

            pos.z += _blockSpawnConfig.OffsetZ;
        }

        previousBlockType = blockType;
        nextBlockType = block.GetBlockType(previousBlockType);

        _transitionTile = GetTransitionTile(_tiles, previousBlockType, nextBlockType, ref pos);

        pos.z += _blockSpawnConfig.OffsetZ;
    }
    private Tile GetTransitionTile(Tile[] tiles, eBlockType previousBlockType, eBlockType nextBlockType, ref Vector3 pos)
    {
        var lastIndex = tiles.Length - 1;
        switch (previousBlockType)
        {
            case eBlockType.City:
                switch (nextBlockType)
                {
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                }
                break;
            case eBlockType.Desert:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                }
                break;
            case eBlockType.Forest:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                }
                break;
            case eBlockType.Highway:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficConfig, _blockType);
                        break;
                }
                break;
        }

        return tiles[lastIndex];
    }
    private Tile CreateTransitionTileAsGameObject(Tile obj, ref Vector3 pos)
    {
        return Instantiate(obj, pos, obj.transform.rotation, transform);
    }

    public BoxCollider GetTransitionTileCollider()
    {
        return _transitionTile.GetComponent<BoxCollider>();
    }
}