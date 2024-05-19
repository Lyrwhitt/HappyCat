using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemCreator : MonoBehaviour
{
    public Dictionary<int, ItemSO> itemData;

    private void Awake()
    {
        InitItemCreator();
    }

    public void InitItemCreator()
    {
        itemData = new Dictionary<int, ItemSO>();
        //itemPrefab = Resources.Load<GameObject>("Items/Prefab/Item");

        LoadItemData();
    }

    public void LoadItemData()
    {
        ItemSO[] datas = Resources.LoadAll<ItemSO>("Items/Data");

        for (int i = 0; i < datas.Length; i++)
        {
            ItemSO item = datas[i];

            itemData[item.itemId] = item;
        }
    }

    public void CreateItem(int itemId, int quantity, Vector3 createPos)
    {
        if (!itemData.ContainsKey(itemId))
            return;

        Item newItem = new Item(itemData[itemId], quantity);

        GameObject newItemObj = ObjectPool.Instance.SpawnFromPool("Item", createPos, Quaternion.identity);
        newItemObj.GetComponent<ItemController>().SetItem(newItem);
    }
}
