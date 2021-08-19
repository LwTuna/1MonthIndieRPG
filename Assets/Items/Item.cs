using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public String name;
    public readonly Sprite _sprite;
    private  int maxStackSize;
    private int currentStackSize;

    private List<ItemComponent> _components;

    public readonly ItemDatabase.ItemType _type;

    public Item(string name, Sprite sprite, int maxStackSize, int currentStackSize, List<ItemComponent> components, ItemDatabase.ItemType type)
    {
        this.name = name;
        _sprite = sprite;
        this.maxStackSize = maxStackSize;
        this.currentStackSize = currentStackSize;
        _components = components;
        _type = type;
    }

    public void OnUse(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item,LayerMask interactLayer)
    {
        
        foreach (var itemComponent in _components)
        {
            
            itemComponent.OnUse(playerController,mouseInWorld,direction,item,interactLayer);
        }
    }

    

    public void UpdateWhileSelected(PlayerController playerController, Vector2 mouseInWorld,
        Direction direction, Item item)
    {
        foreach (var itemComponent in _components)
        {
            itemComponent.UpdateWhileSelected(playerController,mouseInWorld,direction,item);
        }
    }

    public bool SetAmount(int amount)
    {
        currentStackSize = amount;
        return true;
    }

    public int GetAmount()
    {
        return currentStackSize;
    }

    public bool AdjustAmount(int diff)
    {
        return SetAmount(GetAmount() + diff);
    }
    
    public bool IsStackable()
    {
        return maxStackSize > 1;
    }

    public T GetComponent<T>() where T:ItemComponent
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var component in _components)
        {
            if(component.GetType() == typeof(T))
            {
                return (T) component;
            }
        }

        return null;
    }

    public bool HasComponent<T>() where T : ItemComponent
    {
        return GetComponent<T>() != null;
    }
}
