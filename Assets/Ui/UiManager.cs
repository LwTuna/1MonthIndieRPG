using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour,IInventoryUpdateHandler
{
    public GameObject InventoryUi;
    public GameObject CraftingUi;

    public OpenableUi OpenableInventory, OpenableCraftingWindow;

    public Dictionary<Ui, KeyCode> uiHotkeys;
    
    public enum Ui
    {
        Inventory,Crafting,None
    }

    private Ui currentUi = Ui.None;


    private void Start()
    {
        uiHotkeys = new Dictionary<Ui, KeyCode>();
        uiHotkeys[Ui.Inventory] = KeyCode.I;
        uiHotkeys[Ui.Crafting] = KeyCode.C;
    }

    public void Update()
    {
        foreach (var pairs in uiHotkeys)
        {
            if (!Input.GetKeyDown(pairs.Value)) continue;
            if (currentUi != pairs.Key)
            {
                OpenUi(pairs.Key);
            }
            else
            {
                CloseUi();
            }
            break;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentUi != Ui.None)
        {
            CloseUi();
        }
    }


    public void OpenUi(Ui ui)
    {
        CloseUi();
        EnableUi(ui);
        LoadSpecificUiElements(ui);
    }

    public void UpdateUi(Ui ui)
    {
        LoadSpecificUiElements(ui);
    }
    
    private void LoadSpecificUiElements(Ui ui)
    {

        switch (ui)
        {
            case Ui.Inventory:
                OpenableInventory.LoadContents();
                break;
            case Ui.Crafting:
                OpenableCraftingWindow.LoadContents();
                break;
                
            case Ui.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ui), ui, null);
        }
    }

    

    private void EnableUi(Ui ui)
    {
        currentUi = ui;
        switch (ui)
        {
            case Ui.Inventory:
                InventoryUi.SetActive(true);
                break;
            case Ui.Crafting:
                CraftingUi.SetActive(true);
                break;
            case Ui.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ui), ui, null);
        }
    }

    private void CloseUi()
    {
        
        switch (currentUi)
        {
            case Ui.Inventory:
                InventoryUi.SetActive(false);
                break;
            case Ui.Crafting:
                CraftingUi.SetActive(false);
                break;
            case Ui.None:
                break;
            default:
                break;
        }
        currentUi = Ui.None;
        
    }

    public void OnInventoryUpdate(Inventory inventory)
    {
        LoadSpecificUiElements(currentUi);
    }
}
