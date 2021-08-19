using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recipe
{

    public readonly List<CraftingMaterial> needed;
    public readonly CraftingMaterial output;

    public readonly int id;
    private static int nextId = 0;

    public Recipe(List<CraftingMaterial> needed, CraftingMaterial output)
    {
        this.needed = needed;
        this.output = output;
        id = nextId++;
    }

    public bool HasIngredients(Inventory inventory)
    {
        foreach (var mat in needed)
        {
            if (!inventory.Contains(mat.type, mat.amount)) return false;
        }

        return true;
    }

    private void RemoveItems(Inventory inventory)
    {
        foreach (var material in needed)
        {
            inventory.RemoveItem(material.type, material.amount);
        }
    }

    public void CraftItem(Inventory inventory)
    {
        if (!HasIngredients(inventory)) return;
        RemoveItems(inventory);
        
        inventory.AddItem(ItemDatabase.CreateItem(output.type,output.amount));
        
    }
    

    public class CraftingMaterial
    {
        public ItemDatabase.ItemType type;
        public int amount;
        public CraftingMaterial(ItemDatabase.ItemType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }
    
}
