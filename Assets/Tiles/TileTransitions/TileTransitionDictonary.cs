using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileTransitionDictonary : MonoBehaviour
{
    public TileTransitionContainer[] _tileTransitionContainers;


    public TileTransitionContainer GetById(int tileId,int adjacentId)
    {
        return _tileTransitionContainers.FirstOrDefault(tileContainer => tileContainer.tileId == tileId && tileContainer.adjacentTileId == adjacentId);
    }
}
