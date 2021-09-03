using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPickupable : Interactable
{
    
    public List<ItemDrop> _itemDrops;
    public override void OnInteractWithoutItem(PlayerController playerController)
    {
        foreach (var itemDrop in _itemDrops)
        {
            for (int i = 0; i < (int) itemDrop.dropChance;i++)
            {
                InWorldItem.SpawnItemInWorld(gameObject.transform.position, ItemDatabase.CreateItem(itemDrop._type, 1),
                    4f);
            }

            if (itemDrop.dropChance - (int) itemDrop.dropChance < Random.value)
            {
                InWorldItem.SpawnItemInWorld(gameObject.transform.position, ItemDatabase.CreateItem(itemDrop._type, 1),
                    4f);
            }
        }
        
        Destroy(gameObject);
    }

    public override void OnInteract(PlayerController playerController, Item item)
    {
        throw new System.NotImplementedException();
    }

    public override bool CanInteract(PlayerController playerController, Item item)
    {
        throw new System.NotImplementedException();
    }

    public override bool ShouldPlayAnimation(PlayerController playerController, Item item)
    {
        throw new System.NotImplementedException();
    }
}
