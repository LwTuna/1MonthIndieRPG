using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PickaxeComponent : ItemComponent
{
    private int pickaxePower;
    private float pickaxeRange;

    public PickaxeComponent(int pickaxePower, float pickaxeRange)
    {
        this.pickaxePower = pickaxePower;
        this.pickaxeRange = pickaxeRange;
    }

    public override void OnUse(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item,LayerMask interactLayer)
    {
        if (item.HasComponent<CooldownComponent>())
        {
            if(item.GetComponent<CooldownComponent>().HasCooldown()) return;
        }
        Collider2D[] colliders = GetCollissions(playerController,direction,pickaxeRange,interactLayer);
        foreach (var collider2D in colliders)
        {
            
            var interactable = collider2D.gameObject.GetComponent<Interactable>();
            if (interactable == null) continue;
            
            if (!interactable.CanInteract(playerController, item)) continue;
            interactable.OnInteract(playerController,item);
            if (interactable.ShouldPlayAnimation(playerController, item))
            {
                //TODO play anim
            }
            break;
            
        }
    }

    public override void UpdateWhileSelected(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item)
    {
        
    }
    
}
