using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public ItemRarity itemRarity;

    public int itemId;
    public string itemName;
    [TextArea(4, 10)] public string itemDescription;
    public Sprite itemImg;

    public float coolDown;

    [ShowIf("itemType", ItemType.Recovery)]
    public float recoveryAmount;
}
