using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[Serializable]
public class AdjacentTile
{
    
    public TileBase tile;
    public int bitmaskId;
    public bool solid;
}
