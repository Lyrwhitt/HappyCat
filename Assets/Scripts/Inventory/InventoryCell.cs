using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Item item;
    public Image itemImg;
    public Button itemBtn;
    public TextMeshProUGUI quantity;

    private void Start()
    {
        itemBtn.onClick.AddListener(ItemBtnClicked);
    }

    public void ClearCell()
    {
        item = null;
        itemImg.sprite = null;
        quantity.text = string.Empty;
    }

    private void ItemBtnClicked()
    {
        if (item == null)
            return;
        else
        {
            GameObject detailUI = UIManager.Instance.ShowUI("ItemDetail");
            detailUI.GetComponent<ItemDetail>().SetItemDetail(item);

            /*
            ItemDetail itemDetail = Resources.Load<ItemDetail>("UI/ItemDetail");
            itemDetail.SetItemDetail(item);

            Instantiate(itemDetail);
            */
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        itemImg.sprite = item.itemData.itemImg;
        quantity.text = item.quantity.ToString();
    }
}
