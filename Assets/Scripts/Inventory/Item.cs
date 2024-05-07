using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemSO itemData;

    public int quantity;

    //public ICommand useItemCommand;

    public Item(ItemSO itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

public class SaveItemData
{
    public int cellNum;
    public int id;
    public int quantity;

    public SaveItemData(int cellNum, int id, int quantity)
    {
        this.cellNum = cellNum;
        this.id = id;
        this.quantity = quantity;
    }
}
