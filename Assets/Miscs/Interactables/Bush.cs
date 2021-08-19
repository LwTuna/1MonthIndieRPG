using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Bush : Interactable
{
   
    
    public List<ItemDrop> _itemDrops;

    public Sprite empty;
    public Sprite grownSprite;

    private bool grown;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = grownSprite;
        grown = true;
    }

    public override void OnInteractWithoutItem(PlayerController playerController)
    {
        if (!grown) return;
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

        grown = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = empty;
    }

    public override void OnInteract(PlayerController playerController, Item item)
    {
        
    }

    public override bool CanInteract(PlayerController playerController, Item item)
    {
        return false;
    }

    public override bool ShouldPlayAnimation(PlayerController playerController, Item item)
    {
        return false;
    }
}
