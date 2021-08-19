using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : OpenableUi
{


    public GameObject slotPrefab;
    public GameObject itemPanel;

    public PlayerController playerController;
    

    public override void LoadContents()
    {
        foreach (Transform child in itemPanel.transform) {
            Destroy(child.gameObject);
        }
        foreach (var item in playerController._inventory._items)
        {
            GameObject slot = Instantiate(slotPrefab, itemPanel.transform);
            slot.transform.GetChild(0).Find("Icon").GetComponent<Image>().sprite = item._sprite;
            slot.transform.GetChild(0).Find("Count").GetComponent<Text>().text = item.GetAmount() > 1 ? item.GetAmount().ToString() : "";
        }
    }
}
