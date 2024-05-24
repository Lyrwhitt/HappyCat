using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Item
{
    public ItemSO itemData;

    public int quantity;

    private IItemStrategy useItemStrategy;

    public Item(ItemSO itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        this.useItemStrategy = SetItemStrategy(itemData);
    }

    public void UseItem()
    {
        useItemStrategy.Use(this);
    }

    private IItemStrategy SetItemStrategy(ItemSO itemData)
    {
        switch (itemData.itemType)
        {
            case ItemType.Recovery:
                return new RecoveryItem(GameManager.Instance.player, itemData.recoveryAmount);
            case ItemType.Miscellaneous:
                return new MiscellaneousItem();
            default:
                return new MiscellaneousItem();
        }
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

public enum ItemRarity
{
    Common, Uncommon, Rare, Epic, Legendary
}

public enum ItemType
{
    Recovery, Miscellaneous
}


public class RecoveryItem : IItemStrategy
{
    private Player player;
    private float amount;

    public RecoveryItem(Player player, float amount)
    {
        this.player = player;
        this.amount = amount;
    }

    public void Use(Item item)
    {
        player.status.RecoveryHealth(amount);
        item.quantity -= 1;
    }
}

public class MiscellaneousItem : IItemStrategy
{
    public MiscellaneousItem() { }

    public void Use(Item item)
    {
        return;
    }
}