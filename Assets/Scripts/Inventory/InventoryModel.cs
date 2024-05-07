using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private Dictionary<int, ItemSO> itemData;
    public Dictionary<int, Item> items;

    public void LoadItemData()
    {
        ItemSO[] datas = Resources.LoadAll<ItemSO>("Items");

        for(int i = 0; i < datas.Length; i++)
        {
            ItemSO item = datas[i];

            itemData[item.itemId] = item;
        }
    }

    public void SetInventoryList(List<SaveItemData> saveDatas)
    {
        LoadItemData();

        for (int i = 0; i < saveDatas.Count; i++)
        {
            SaveItemData saveData = saveDatas[i];
            Item item = new Item(itemData[saveData.id], saveData.quantity);

            items[saveData.cellNum] = item;
        }
    }

    /*
    public void AddItem(ItemSO item, int quantity, ICommand useItemCommand)
    {
        Item addItem = items.Find(x => x.itemData.itemId == item.itemId);

        if(addItem != null)
        {
            addItem.quantity += quantity;
        }
        else
        {
            addItem = new Item(item, quantity, useItemCommand);

            items.Add(addItem);
            //inventoryView.AddItem(items.Count - 1, addItem);
        }

        //inventoryView.UpdateInventory(items);
    }
    */

    public void AddItem(Item addItem, int cellNum)
    {
        int itemIdx = SearchInventory(addItem);

        if (itemIdx != -1) 
        {
            items[itemIdx].quantity += addItem.quantity;
        }
        else
        {
            items[cellNum].quantity += addItem.quantity;
        }
    }

    /*
    public void RemoveItem(Item item)
    {
        Item removeItem = items.Find(x=> x.itemData.itemId == item.itemData.itemId);

        if (removeItem != null)
        {
            items.Remove(removeItem);
            //inventoryView.RemoveItem(items.Count - 1);
        }

        //inventoryView.UpdateInventory(items);
    }
    */

    public void RemoveItem(int cellNum)
    {
        items.Remove(cellNum);
    }

    public int SearchInventory(Item searchItem)
    {
        foreach (var item in items)
        {
            if(item.Value.itemData.itemId == searchItem.itemData.itemId)
            {
                return item.Key;
            }
        }

        return -1;
    }
}
