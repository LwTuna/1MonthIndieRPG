using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public static ItemDatabase Instance;


    public GameObject prefabItem;

    public Sprite missing,stone,wood,vine,twig,cotton,fruit,woodenAxe,woodenPickaxe;
    
    public ItemDatabase()
    {
        Instance = this;
    }

    public enum ItemType
    {
        WoodenPickaxe,WoodenAxe,Stone,Wood,Vine,Twig,Cotton,Fruit
    }

    public static Item CreateItem(ItemType type, int amount)
    {
        return type switch
        {
            ItemType.WoodenPickaxe => new Item("Wooden Pickaxe", GetSprite(type), 1, 1,
                new List<ItemComponent>() {new PickaxeComponent(1, 0.5f), new CooldownComponent(1f)},
                ItemType.WoodenPickaxe),
            ItemType.WoodenAxe => new Item("Wooden Axe", GetSprite(type), 1, 1,
                new List<ItemComponent>() {new AxeComponent(1, 0.5f), new CooldownComponent(1f)}, type),
            ItemType.Stone => new Item("Stone", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            ItemType.Wood => new Item("Wood", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            ItemType.Vine => new Item("Vine", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            ItemType.Twig => new Item("Twig", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            ItemType.Cotton => new Item("Cotton", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            ItemType.Fruit => new Item("Fruit", GetSprite(type), 99, amount, new List<ItemComponent>(), type),
            _ => null
        };
    }

    public static Sprite GetSprite(ItemType outputType)
    {
        return outputType switch
        {
            ItemType.WoodenPickaxe => Instance.woodenPickaxe,
            ItemType.WoodenAxe => Instance.woodenAxe,
            ItemType.Stone => Instance.stone,
            ItemType.Wood => Instance.wood,
            ItemType.Vine => Instance.vine,
            ItemType.Twig => Instance.twig,
            ItemType.Cotton =>Instance.cotton ,
            ItemType.Fruit => Instance.fruit,
            _ => Instance.missing
        };
    }
}
