using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUiManager : OpenableUi
{
    private bool isOpen;

    

    public GameObject recipePrefab;
    public GameObject recipePanel;

    public GameObject materialPanel;
    
    public CraftingManager craftingManager;

    public GameObject craftingPanel;

    public PlayerController player;
    
    private Recipe activeRecipe;
    
    
    public void SetActiveRecipe(int id)
    {
        Recipe recipe = craftingManager.GetById(id);
        if (recipe != null)
        {
            activeRecipe = recipe;
            
            foreach (Transform child in materialPanel.transform) {
                Destroy(child.gameObject);
            }
            foreach(Recipe.CraftingMaterial craftingMaterial in recipe.needed)
            {
                GameObject slot = Instantiate(recipePrefab, materialPanel.transform);
                slot.transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = ItemDatabase.GetSprite(craftingMaterial.type);
                slot.transform.GetChild(0).Find("Count").GetComponent<Text>().text = craftingMaterial.amount > 1 ? craftingMaterial.amount.ToString() : "";
            }
            foreach (Transform child in craftingPanel.transform) {
                Destroy(child.gameObject);
            }
            GameObject slo = Instantiate(recipePrefab, craftingPanel.transform);
            slo.transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = ItemDatabase.GetSprite(recipe.output.type);
            slo.transform.GetChild(0).Find("Count").GetComponent<Text>().text = recipe.output.amount > 1 ? recipe.output.amount.ToString() : "";
        }
    }

    public void Craft()
    {
        activeRecipe?.CraftItem(player._inventory);
    }

    public override void LoadContents()
    {
        
        foreach (Transform child in recipePanel.transform) {
            Destroy(child.gameObject);
        }
        foreach (var recipe in craftingManager.GetRecipes())
        {
            GameObject slot = Instantiate(recipePrefab, recipePanel.transform);
            slot.transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = ItemDatabase.GetSprite(recipe.output.type);
            slot.transform.GetChild(0).Find("Count").GetComponent<Text>().text = recipe.output.amount > 1 ? recipe.output.amount.ToString() : "";
            
            slot.transform.GetComponentInChildren<Button>().onClick.AddListener( () => SetActiveRecipe(recipe.id));
        }
    }
}
