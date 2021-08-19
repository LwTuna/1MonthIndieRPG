using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Choppable : Interactable
{
    
    public List<ItemDrop> _itemDrops;

    public Sprite stump,grown;


    private bool growState;
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = grown;
        growState = true;
    }

    public override void OnInteractWithoutItem(PlayerController playerController)
    {
        
    }

    public override void OnInteract(PlayerController playerController, Item item)
    {
        if(!growState) return;
            
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

        gameObject.GetComponent<SpriteRenderer>().sprite = stump;
        growState = false;
    }

    public override bool CanInteract(PlayerController playerController, Item item)
    {
        return item.HasComponent<AxeComponent>();
    }

    public override bool ShouldPlayAnimation(PlayerController playerController, Item item)
    {
        return item.HasComponent<AxeComponent>();
    }
}
