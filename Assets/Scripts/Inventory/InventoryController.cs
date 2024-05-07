using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private DataManager<List<SaveItemData>> inventoryDataManager;
    private List<SaveItemData> inventoryData;

    private InventoryView inventoryView;
    private InventoryModel inventoryModel;

    private void Awake()
    {
        inventoryView = this.GetComponent<InventoryView>();
    }

    private void Start()
    {
        inventoryModel = new InventoryModel();
        inventoryModel.SetInventoryList(inventoryData);

        inventoryView.InitializeInventoryView(inventoryModel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemSO addItem = Resources.Load<ItemSO>("Item/TestItem");
            Debug.Log("Add Item : " + addItem.name);
            //inventoryModel.AddItem(addItem, 1, new NoEffect());
            inventoryView.UpdateInventory();
        }
    }

    private void OnApplicationQuit()
    {
        SaveInventoryData();
    }

    private void SaveInventoryData()
    {
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
