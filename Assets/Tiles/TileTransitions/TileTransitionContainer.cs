using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class TileTransitionContainer
{
    public int tileId,adjacentTileId;
    public List<AdjacentTile> adjacentTiles;


    public AdjacentTile GetByBitmask(int bitmask)
    {
        return adjacentTiles.FirstOrDefault(adjacentTile => adjacentTile.bitmaskId == bitmask);
    }
}
