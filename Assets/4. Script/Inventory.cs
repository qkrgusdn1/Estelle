using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<InventroyItemPanel> inventroyItemPanels = new List<InventroyItemPanel>();

    Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();


    private void Start()
    {
        items.Add("Photato", new InventoryItem("Photato"));
        items.Add("SweetPotato", new InventoryItem("SweetPotato"));
        items.Add("Pork", new InventoryItem("Pork"));

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem("Photato");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            AddItem("SweetPotato");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem("Pork");
        }
    }

    public void AddItem(string key)
    {
        if (items.ContainsKey(key) == false)
        {
            items.Add(key, new InventoryItem(key));

        }
        items[key].count++;

        
        foreach (var data in items)
        {
            Debug.Log(data.Key);
            Debug.Log(data.Value.count);
        }

        foreach (var panel in inventroyItemPanels)
        {
            if (items.ContainsKey(panel.nameText.text))
            {
                panel.SetInventoryItem(items[panel.nameText.text]);
            }
        }
    }

}

[System.Serializable]
public class InventoryItem
{
    public InventoryItem(string name)
    {
        this.name = name;
        count = 0;

    }
    public string name;
    public int count;
}
