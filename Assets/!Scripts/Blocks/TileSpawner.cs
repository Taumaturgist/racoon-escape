using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficSpawnConfig _trafficSpawnConfig;

    private Tile[] _tiles;
    private Tile[] _tileSet;
    private Tile _transitionTile;
    private bool _isFirstBlock;

    private const int _maxBlockAmountInScene = 2;

    public void Launch(BlockSpawnConfig blockSpawnConfig, BuildingSpawnConfig buildingSpawnConfig,
        TrafficSpawnConfig trafficSpawnConfig, Block block,
        ref eBlockType previousBlockType, ref eBlockType nextBlockType,
        ref Vector3 pos, Quaternion rot, bool isFirstBlock)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficSpawnConfig = trafficSpawnConfig;
        _isFirstBlock = isFirstBlock;

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
            if (crossroadCount < _maxBlockAmountInScene)
            {
                if (i == 0 && _isFirstBlock)
                {
                    _tiles[i] = Instantiate(_blockSpawnConfig.FirstTileInFirstBlock, pos, rot, transform);
                    _tiles[i].Launch(_buildingSpawnConfig, _trafficSpawnConfig, _isFirstBlock);
                }
                else
                {
                    _tiles[i] = Instantiate(_tileSet[randomIndex], pos, rot, transform);

                    _tiles[i].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);

                    if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                        crossroadCount++;
                }
            }
            else
            {
                // Crossroad index = 0
                randomIndex = Random.Range(_blockSpawnConfig.CrossroadNumberInCity, _tileSet.Length);

                _tiles[i] = Instantiate(_tileSet[randomIndex], pos, rot, transform);

                _tiles[i].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
            }

            pos.z += _blockSpawnConfig.OffsetZ;
        }

        previousBlockType = blockType;
        nextBlockType = block.GetBlockType(previousBlockType);

        _transitionTile = GetTransitionTile(_tiles, previousBlockType, nextBlockType, ref pos);

        pos.z += _blockSpawnConfig.OffsetZ;
    }

    private Tile GetTransitionTile(Tile[] tiles, eBlockType previousBlockType, eBlockType nextBlockType,
        ref Vector3 pos)
    {
        var lastIndex = tiles.Length - 1;
        switch (previousBlockType)
        {
            case eBlockType.City:
                switch (nextBlockType)
                {
                    case eBlockType.Desert:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.CityHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                }

                break;
            case eBlockType.Desert:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                }

                break;
            case eBlockType.Forest:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Highway:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestHighwayTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                }

                break;
            case eBlockType.Highway:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles[lastIndex] = CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayCityTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Desert:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayDesertTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
                        break;
                    case eBlockType.Forest:
                        tiles[lastIndex] =
                            CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayForestTile, ref pos);
                        _tiles[lastIndex].Launch(_buildingSpawnConfig, _trafficSpawnConfig, false);
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