using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class ItemComponent
{

    public abstract void OnUse(PlayerController playerController,Vector2 mouseInWorld,Direction direction,Item item,LayerMask interactLayer);

    public abstract void UpdateWhileSelected(PlayerController playerController, Vector2 mouseInWorld,
        Direction direction, Item item);

    
    protected Collider2D[] GetCollissions(PlayerController playerController,Direction direction,float interactRange,LayerMask interactLayer)
    {
        var collisions = direction switch
        {
            Direction.North => Physics2D.OverlapCircleAll(playerController.interactionNorth.position, interactRange,interactLayer),
            Direction.South => Physics2D.OverlapCircleAll(playerController.interactionSouth.position, interactRange,interactLayer),
            Direction.East => Physics2D.OverlapCircleAll(playerController.interactionEast.position, interactRange,interactLayer),
            Direction.West => Physics2D.OverlapCircleAll(playerController.interactionWest.position, interactRange,interactLayer),
            _ => new Collider2D[0]
        };
        return collisions;
    }
}
