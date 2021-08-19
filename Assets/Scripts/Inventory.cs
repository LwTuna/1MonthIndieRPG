using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Inventory
{
    public readonly List<Item> _items;

    protected List<IInventoryUpdateHandler> InventoryUpdateHandlers = new List<IInventoryUpdateHandler>();
    
    public Inventory()
    {
        _items = new List<Item>();
    }


    public void AddItem(Item item)
    {
        foreach (var i in _items.Where(i => i._type == item._type))
        {
            if(!item.IsStackable()) continue;
            
            i.SetAmount(item.GetAmount() + i.GetAmount());
            OnInventoryUpdate();
            return;
        }

        _items.Add(item);
        
        OnInventoryUpdate();
    }

    
    public void SubscribeToInventoryUpdateEvent(IInventoryUpdateHandler updateHandler)
    {
      InventoryUpdateHandlers.Add(updateHandler);
    }
   
    protected  void OnInventoryUpdate()
    {
        foreach (var updateHandler in InventoryUpdateHandlers)
        {
            updateHandler.OnInventoryUpdate(this);  
        }
      
        //_inventoryManager.UpdateUi(UiManager.Ui.Inventory);
    }
    
    
    public abstract bool Contains(ItemDatabase.ItemType type, int count);
    

    public abstract void RemoveItem(ItemDatabase.ItemType materialType, int materialAmount);
}
