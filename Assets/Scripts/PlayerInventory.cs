using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : Inventory
{
   private Item[] _hotbar = new Item[9];
   private int _currentItem = 0;



   private UiManager _uiManager;
   
   

   public PlayerInventory(UiManager uiManager)
   {
      _uiManager = uiManager;
      SubscribeToInventoryUpdateEvent(uiManager);
   }


   public void SetHotBarItem(Item item, int slot)
   {
      if(slot<0 || slot>9) throw new ArgumentException("Slot can only be between 0 and 8(including) slot:"+slot);
      _hotbar[slot] = item;
      OnInventoryUpdate();
   }
   
   public Item GetCurrentItem()
   {
      return _hotbar[_currentItem];
   }

  
   

   public override void RemoveItem(ItemDatabase.ItemType materialType, int materialAmount)
   {
      foreach (var item in _items.Where(item => item._type == materialType))
      {
         item.AdjustAmount(-materialAmount); 
         if (item.GetAmount() > 0) return;
         _items.Remove(item);
         return;
      }
      foreach (var item in _hotbar)
      {
         if(item== null) continue;
         if (item._type == materialType)
         {
            item.AdjustAmount(-materialAmount);
            if (item.GetAmount() > 0) continue;
            _hotbar = _hotbar.Where(val => val != item).ToArray();
            return;
         }
      }
      OnInventoryUpdate();
   }

   public override bool Contains(ItemDatabase.ItemType type, int count)
   {
      foreach (var item in _items.Where(item => item._type == type))
      {
         if (item.GetAmount() >= count) return true;
      }
      foreach (var item in _hotbar)
      {
         if(item == null) continue;
         if (item._type == type)
         {
            if (item.GetAmount() >= count) return true;
         }
      }

      return false;
   }

   
}
