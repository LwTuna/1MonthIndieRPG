using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitable : Interactable
{
    public override void OnInteractWithoutItem(PlayerController playerController)
    {
        
    }

    public override void OnInteract(PlayerController playerController, Item item)
    {
        gameObject.GetComponent<Health>()?.TakeDamage(item.GetComponent<SwordComponent>().swordPower);
    }

    public override bool CanInteract(PlayerController playerController, Item item)
    {
        return item.HasComponent<SwordComponent>();
    }

    public override bool ShouldPlayAnimation(PlayerController playerController, Item item)
    {
        return item.HasComponent<SwordComponent>();
    }
}
