using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image itemImg;
    public TextMeshProUGUI itemDescriptionText;

    public void SetItemTooltip(Item item)
    {
        itemImg.sprite = item.itemData.itemImg;
        itemNameText.text = item.itemData.itemName;
        itemDescriptionText.text = item.itemData.itemDescription;
    }

    private void ClearItemTooltip()
    {
        itemImg.sprite = null;
        itemNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
    }
}
