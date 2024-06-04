using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private DataManager<List<SaveItemData>> inventoryDataManager;
    private List<SaveItemData> inventoryData;

    public InventoryView inventoryView;
    private InventoryModel inventoryModel;

    private void Awake()
    {
        SetInventoryData();
    }

    private void Start()
    {
        inventoryModel = new InventoryModel();
        inventoryModel.SetInventoryList(inventoryData);

        inventoryView.InitializeInventoryView(inventoryModel.items, this);

        inventoryModel.onAddItem += inventoryView.OnAddItem;
        inventoryModel.onRemoveItem += inventoryView.OnRemoveItem;
        inventoryModel.onSwapItem += inventoryView.OnSwapItem;
    }

    private void OnApplicationQuit()
    {
        SaveInventoryData();
    }

    public void AddInventoryItem(Item addItem)
    {
        inventoryModel.AddItem(addItem);
    }

    public void RemoveInventoryItem(int removeIdx)
    {
        inventoryModel.RemoveItem(removeIdx);
    }

    public void SwapInventoryItem(int swapIdx1, int swapIdx2)
    {
        inventoryModel.SwapItem(swapIdx1, swapIdx2);
    }

    private void SetInventoryData()
    {
        inventoryDataManager = new DataManager<List<SaveItemData>>(Path.Combine(Application.persistentDataPath, "InventoryData.json"));
        inventoryData = inventoryDataManager.LoadData();

        if (inventoryData == null)
        {
            inventoryData = new List<SaveItemData>();
        }
    }


    private void SaveInventoryData()
    {
        inventoryData.Clear();

        for (int i = 0; i < inventoryView.cells.Count; i++)
        {
            Item item = inventoryView.cells[i].item;

            if (item != null)
            {
                SaveItemData saveData = new SaveItemData(i, item.itemData.itemId, item.quantity);
                inventoryData.Add(saveData);
            }
        }

        inventoryDataManager.SaveData(inventoryData);
    }
}
