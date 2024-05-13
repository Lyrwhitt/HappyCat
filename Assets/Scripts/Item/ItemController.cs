using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return this.item;
    }
}
