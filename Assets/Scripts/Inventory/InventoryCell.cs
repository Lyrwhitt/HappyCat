using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public Image itemImg;
    public Button itemBtn;
    public TextMeshProUGUI quantity;

    private ItemTooltip itemTooltip;
    private RectTransform rectTransform;

    private Vector2 offset = new Vector2(470f, -100f);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        itemBtn.onClick.AddListener(ItemBtnClicked);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;

        itemTooltip = ObjectPool.Instance.SpawnFromPool("ItemTooltip", Vector3.zero, Quaternion.identity,
            UIManager.Instance.canvas.transform).GetComponent<ItemTooltip>();

        Vector2 position = rectTransform.position;
        position += offset;

        RectTransform canvasRectTransform = UIManager.Instance.canvas.GetComponent<RectTransform>();
        RectTransform tooltipRectTransform = itemTooltip.GetComponent<RectTransform>();

        Vector2 tooltipSize = tooltipRectTransform.sizeDelta;

        if (position.x + tooltipSize.x > canvasRectTransform.rect.width)
        {
            position.x = canvasRectTransform.rect.width - tooltipSize.x;
        }
        if (position.y + tooltipSize.y > canvasRectTransform.rect.height)
        {
            position.y = canvasRectTransform.rect.height - tooltipSize.y;
        }

        itemTooltip.transform.position = position;

        itemTooltip.SetItemTooltip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemTooltip == null)
            return;

        ObjectPool.Instance.ReturnToPool("ItemTooltip", itemTooltip.gameObject);
        itemTooltip = null;
    }
    
    public void ClearCell()
    {
        item = null;
        itemImg.sprite = null;
        quantity.text = string.Empty;

        itemImg.gameObject.SetActive(false);
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

        itemImg.gameObject.SetActive(true);
    }

}
