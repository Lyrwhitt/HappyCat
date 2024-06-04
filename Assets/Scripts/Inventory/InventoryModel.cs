using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryModel
{
    private Dictionary<int, ItemSO> itemData = new Dictionary<int, ItemSO>();
    //public SortedDictionary<int, Item> items = new SortedDictionary<int, Item>();

    public int inventoryLimit = 21;
    public Item[] items = new Item[30];

    public event Action<int, Item> onAddItem;
    public event Action<int> onRemoveItem;
    public event Action<int, int> onSwapItem;

    public void LoadItemData()
    {
        ItemSO[] datas = Resources.LoadAll<ItemSO>("Items/Data");

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

    public void AddItem(Item addItem)
    {
        /*
        int itemIdx = SearchInventory(addItem);

        if (items.ContainsKey(itemIdx))
        {
            items[itemIdx].quantity += addItem.quantity;
        }
        else
        {
            items[itemIdx] = addItem;
        }
        */

        int itemIdx = SearchInventory(addItem);

        if(itemIdx > inventoryLimit)
        {
            return;
        }

        if (items[itemIdx] != null)
        {
            items[itemIdx].quantity += addItem.quantity;
        }
        else
        {
            items[itemIdx] = addItem;
        }

        onAddItem?.Invoke(itemIdx, items[itemIdx]);
    }

    public void RemoveItem(int removeIdx)
    {
        //items.Remove(removeIdx);

        items[removeIdx] = null;

        onRemoveItem?.Invoke(removeIdx);
    }

    public void SwapItem(int origin, int swap)
    {
        /*
        if (!items.ContainsKey(swap))
        {
            items[swap] = items[origin];
            items.Remove(origin);
        }
        else
        {
            Item swapItem = items[origin];
            items[origin] = items[swap];
            items[swap] = swapItem;
        }
        */

        if (items[swap] == null)
        {
            items[swap] = items[origin];
            items[origin] = null;
        }
        else
        {
            Item swapItem = items[origin];
            items[origin] = items[swap];
            items[swap] = swapItem;
        }

        onSwapItem?.Invoke(origin, swap);
    }

    public int SearchInventory(Item searchItem)
    {
        /*
        int minIdx = 0; 
        
        foreach (var item in items)
        {
            if(item.Value.itemData.itemId == searchItem.itemData.itemId)
            {
                return item.Key;
            }

            if(item.Key == minIdx)
            {
                minIdx += 1;
            }
        }
        
        return minIdx;
        */

        int minIdx = 9999;

        for (int i = 0; i < inventoryLimit; i++)
        {
            Item item = items[i];

            if (item == null)
            {
                if(i <= minIdx)
                    minIdx = i;

                continue;
            }

            if (item.itemData.itemId == searchItem.itemData.itemId)
            {
                return i;
            }
        }

        return minIdx;
    }
}
