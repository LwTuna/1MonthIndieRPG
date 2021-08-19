using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordComponent : ItemComponent
{
    
    public int swordPower;
    private float swordRange;

    public SwordComponent(int swordPower, float swordRange)
    {
        this.swordPower = swordPower;
        this.swordRange = swordRange;
    }

    public override void OnUse(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item,
        LayerMask interactLayer)
    {
        if (item.HasComponent<CooldownComponent>())
        {
            if(item.GetComponent<CooldownComponent>().HasCooldown()) return;
        }
        Collider2D[] colliders = GetCollissions(playerController,direction,swordRange,interactLayer);
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
