using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public static Inventory inventoryInstance;
    public int inventorySize = 20;

    private void Awake()
    {
        if(inventoryInstance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        inventoryInstance = this;
    }

    public void add(Item item)
    {
        items.Add(item);
    }
    public void remove(Item item)
    {
        items.Remove(item);
    }
}
