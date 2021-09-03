using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTransitionManager:MonoBehaviour
{


    private TileTransitionDictonary _tileTransitionDictonary;


    private void Awake()
    {
        _tileTransitionDictonary = GetComponent<TileTransitionDictonary>();
    }

    
    public void ApplyBitmask(TileTransitionData[,] data, Tilemap walkable, Tilemap solid)
    {

        for (int x = 0; x < data.GetLength(0); x++)
        {
            for (int y = 0; y < data.GetLength(1); y++)
            {
                foreach (var entry in data[x,y].bitmask)
                {
                    var container = _tileTransitionDictonary.GetById(data[x, y].self, entry.Key);

                    var adjacentTile = container?.GetByBitmask(entry.Value);
                    if(adjacentTile == null) continue;
                    var tilePos = new Vector3Int(x, y, 0);
                    walkable.SetTile(tilePos,null);
                    solid.SetTile(tilePos,null);
                    (adjacentTile.solid ? solid : walkable).SetTile(tilePos,adjacentTile.tile);
                }
                
            }
        }
    }


    
    
    
    
    

    public static TileTransitionData[,] CreateBitmask(int[,] map)
    {
        var data = new TileTransitionData[map.GetLength(0),map.GetLength(1)];
        
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var adjacent = PrepareAdjacentArray(x, y, map);
                data[x,y] = new TileTransitionData(adjacent,map[x,y]);
            }
        }

        return data;
    }
    /*
    * 0,1,2
    * 3,  4
    * 5,6,7
    * 
    */
    private static int[] PrepareAdjacentArray(int x, int y, int[,] map)
    {
        var adjacent = new int[8];
        adjacent[0] = GetAdjacentTile(x, y, map, -1, +1);
        adjacent[1] = GetAdjacentTile(x, y, map, 0, +1);
        adjacent[2] = GetAdjacentTile(x, y, map, 1, +1);
        adjacent[3] = GetAdjacentTile(x, y, map, -1, 0);
        adjacent[4] = GetAdjacentTile(x, y, map, 1, 0);
        adjacent[5] = GetAdjacentTile(x, y, map, -1, -1);
        adjacent[6] = GetAdjacentTile(x, y, map, 0, -1);
        adjacent[7] = GetAdjacentTile(x, y, map, 1, -1);
        return adjacent;
    }


    private static int GetAdjacentTile(int x, int y, int[,] map,int xOffset,int yOffset)
    {
        if (x +xOffset <= 0 || x + xOffset >= map.GetLength(0) || y + yOffset <= 0 || y + yOffset >= map.GetLength(1))
        {
            return map[x, y];
        }

        return map[x + xOffset, y + yOffset];
        
    }
}
    

