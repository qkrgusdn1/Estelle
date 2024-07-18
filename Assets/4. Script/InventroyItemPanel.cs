using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventroyItemPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text countText;


    InventoryItem inventoryItem;
    public void SetInventoryItem(InventoryItem item)
    {
        Debug.Log("SetInventoryItem");
        inventoryItem = item;
        nameText.text = inventoryItem.name;
        UpdatePanel();
    }

    void UpdatePanel()
    {
        countText.text = inventoryItem.count.ToString();
    }
}
