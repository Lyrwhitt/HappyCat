using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDropItem : MonoBehaviour
{
    private Item item;

    public Image itemIgm;

    public void SetItem(Item item)
    {
        this.item = item;
        itemIgm.sprite = item.itemData.itemImg;
    }

    public Item GetItem()
    {
        return this.item;
    }
}
