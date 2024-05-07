using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public int itemId;
    public string itemName;
    [TextArea(4, 10)] public string itemDescription;
    public Sprite itemImg;
}
