using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private int maxSize = 16;

    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    
    private Action<Item> useItemAction;

    public Inventory(Action<Item>  useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
    }

    public int CountItemsByType(Item.ItemType type)
    {
        int sum = 0;
        foreach(Item item in GetItemsByType(type))
        {
            sum += item.amount;    
        }
        return sum;

    }

    public void RemoveItemsByType(Item.ItemType type)
    {
        itemList.RemoveAll(item => item.itemType == type);
   
    }

    public List<Item> GetItemsByType(Item.ItemType type)
    {
        return itemList.FindAll(item => item.itemType == type);

    }

    public bool AddItem(Item item)
    {

        if (itemList.Count == maxSize) return false;

        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory) itemList.Add(item); 
        } else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = item;
                }
            }

            if (itemInInventory != null) itemList.Remove(item);
        }
        else
        {
            itemList.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    public void UseItem(Item item)
    {
        if (item.IsConsumable()) { 
            useItemAction(item);
            RemoveItem(item);
        }
    }

    public IEnumerable<Item> GetItemList()
    {
        return itemList;
    }
}
