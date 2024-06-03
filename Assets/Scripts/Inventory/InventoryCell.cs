using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private int cellNum = 0;

    public Item item;
    public Image itemImg;
    public TextMeshProUGUI quantity;

    private CellButton itemBtn;
    private ItemTooltip itemTooltip;
    private RectTransform rectTransform;

    private Vector2 offset = new Vector2(250f, -125f);

    private DragDropItem dragDropItem;
    private RectTransform dragDropRectTransform;
    private CanvasGroup itemImgCG;

    public TextMeshProUGUI cooldownText;
    private float cooldown = 0f;
    private GameObject cooldownObj;
    private Image cooldownImg;
    private bool itemUsable = true;

    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        itemImgCG = itemImg.GetComponent<CanvasGroup>();
        itemBtn = GetComponent<CellButton>();
    }

    private void Start()
    {
        itemBtn.onClick += ItemBtnClicked;

        cooldownObj = cooldownText.transform.parent.gameObject;
        cooldownObj.SetActive(false);
        cooldownImg = cooldownObj.GetComponent<Image>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || isDragging)
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
            //position.x = canvasRectTransform.rect.width - tooltipSize.x;
            position.x = rectTransform.position.x - offset.x;
        }
        if (position.y + tooltipSize.y > canvasRectTransform.rect.height)
        {
            //position.y = canvasRectTransform.rect.height - tooltipSize.y;
            position.y = rectTransform.position.y - offset.y;
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDropItem = Instantiate(Resources.Load<DragDropItem>("UI/Inventory/DragDropItem"), UIManager.Instance.canvas.transform);
        dragDropRectTransform = dragDropItem.GetComponent<RectTransform>();
        dragDropItem.SetItem(item);

        itemImgCG.alpha = 0.6f;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragDropRectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImgCG.alpha = 1f;
        isDragging = false; 

        if (eventData.pointerEnter != null && eventData.pointerEnter.tag == "ShortcutSlot")
        {
            InventoryCell inventoryCell = eventData.pointerEnter.GetComponent<InventoryCell>();

            /*
            if(inventoryCell.item != null)
            {
                Item changeItem = inventoryCell.item;
                inventoryCell.SetItem(this.item);
                SetItem(changeItem);
            }
            else
            {
                inventoryCell.SetItem(item);
                ClearCell();
            }
            */
            GameManager.Instance.player.inventoryController.SwapInventoryItem(cellNum, inventoryCell.cellNum);

            dragDropRectTransform.position = eventData.pointerEnter.transform.position;

            Destroy(dragDropItem.gameObject);
        }
        else
        {
            Destroy(dragDropItem.gameObject);
            // юс╫ц
            //ClearCell();
            GameManager.Instance.player.inventoryController.RemoveInventoryItem(cellNum);
        }
    }

    public void SetCell(int cellNum)
    {
        this.cellNum = cellNum;
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
        if (item == null || !itemUsable)
            return;
        else
        {
            item.UseItem();

            UpdateCell();
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        itemImg.sprite = item.itemData.itemImg;
        quantity.text = item.quantity.ToString();

        itemImg.gameObject.SetActive(true);
    }

    private void UpdateCell()
    {
        if (item.quantity <= 0)
            ClearCell();
        else
        {
            SetCooldown(item.itemData.coolDown);
            quantity.text = item.quantity.ToString();
        }
    }

    private void SetCooldown(float cooldown)
    {
        if (itemUsable)
            CoroutineManager.StartCoroutineStatic(ItemCooldown(cooldown));
    }

    private IEnumerator ItemCooldown(float cooldown)
    {
        itemUsable = false;
        this.cooldown = cooldown;
        cooldownObj.SetActive(true);

        while (this.cooldown > 0f)
        {
            this.cooldown -= Time.deltaTime;

            cooldownText.SetText(Mathf.FloorToInt(this.cooldown + 1).ToString());
            cooldownImg.fillAmount = this.cooldown / cooldown;

            yield return null;
        }

        itemUsable = true;
        cooldownObj.SetActive(false);
    }
}
