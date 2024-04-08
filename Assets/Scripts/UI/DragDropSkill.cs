using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSkill : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private UISkillInfo skillInfo;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //private Vector3 originalPos;

    private DragDropSkillItem dragDropItem;

    private void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        skillInfo = this.transform.parent.GetComponent<UISkillInfo>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //originalPos = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDropItem = Instantiate(Resources.Load<DragDropSkillItem>("UI/SkillItem"), UIManager.Instance.canvas.transform);
        rectTransform = dragDropItem.GetComponent<RectTransform>();
        dragDropItem.skillSO = skillInfo.skillSO;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.tag == "ShortcutSlot")
        {
            //rectTransform.position = eventData.pointerEnter.transform.position;
            SkillCell skillCell = eventData.pointerEnter.GetComponent<SkillCell>();

            if (skillCell.skill != null)
            {
                skillCell.RemoveSkill();
            }

            skillCell.SetSkill(dragDropItem);

            dragDropItem.canvasGroup.blocksRaycasts = true;
            rectTransform.position = eventData.pointerEnter.transform.position;
        }
        else
        {
            //rectTransform.position = originalPos;
            Destroy(dragDropItem.gameObject);
        }
    }
}
