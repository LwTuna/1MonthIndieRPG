using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager:MonoBehaviour
{
    private List<Recipe> _recipes;
    public void Awake()
    {
        _recipes = new List<Recipe>();
        _recipes.Add(new Recipe(new List<Recipe.CraftingMaterial>(){new Recipe.CraftingMaterial(ItemDatabase.ItemType.Twig,1),new Recipe.CraftingMaterial(ItemDatabase.ItemType.Vine,1),new Recipe.CraftingMaterial(ItemDatabase.ItemType.Wood,3)},new Recipe.CraftingMaterial(ItemDatabase.ItemType.WoodenPickaxe,1)));
    }


    public List<Recipe> GetRecipes()
    {
        return _recipes;
    }

    public Recipe GetById(int id)
    {
        return _recipes.FirstOrDefault(recipe => recipe.id == id);
    }
}
