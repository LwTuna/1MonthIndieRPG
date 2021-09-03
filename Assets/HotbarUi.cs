using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUi : MonoBehaviour, IInventoryUpdateHandler
{

    public GameObject[] hotbarSlots;
    // Start is called before the first frame update
    private PlayerInventory _playerInventory;
    private int currentHig = 8;
    void Start()
    {
        _playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerController>()._inventory;
        _playerInventory.SubscribeToInventoryUpdateEvent(this);
        UpdateHotbar(_playerInventory);
        
        SetHighlighted(0);
    }

    public void SetHighlighted(int id)
    {
        if(currentHig == id) return;
        hotbarSlots[id].GetComponent<Image>().color = Color.yellow;
        hotbarSlots[currentHig].GetComponent<Image>().color = Color.white;

        currentHig = id;
    }

    public void OnInventoryUpdate(Inventory inventory)
    {
        if (inventory is PlayerInventory playerInventory)
        {
            UpdateHotbar(playerInventory);
        }
    }

    public void Update()
    {
        int change = -(int) Input.mouseScrollDelta.y;
        _playerInventory.ScrollHotbar(change);
        
        
        
        if (change != 0 || CheckKeys())
        {
            SetHighlighted(_playerInventory.GetCurrentHotbarSlot());
        }

        

    }

    private bool CheckKeys()
    {

        for (var i = 0; i < 9; i++)
        {
            if (!Input.GetKeyDown(KeyCode.Alpha1 + i)) continue;
            _playerInventory.SetCurrentHotbarSlot(i);
            return true;
        }
      
        return false;
    }

    private void UpdateHotbar(PlayerInventory inventory)
    {
        Item[] hotBarItems = inventory.GetHotBarItems();

        for (var i = 0; i < hotBarItems.Length; i++)
        {
            hotbarSlots[i].transform.GetChild(0).gameObject.SetActive(hotBarItems[i] != null);
            if (hotBarItems[i] != null)
            {
                hotbarSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = hotBarItems[i]._sprite;
            }
        }
    }
}
