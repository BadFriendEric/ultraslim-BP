﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public static Inventory inventoryInstance;
    public int inventorySize = 20;
    public GameObject inventoryCanvas;

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
        if (items.Count <= inventorySize)
        {
            items.Add(item);
        }
    }
    public void remove(Item item)
    {
        items.Remove(item);
    }

    public void openCloseInv()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}
