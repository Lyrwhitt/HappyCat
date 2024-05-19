using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSkill : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private UISkillInfo skillInfo;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //private Vector3 originalPos;

    private DragDropSkillItem dragDropItem;

    private void Awake()
    {
        skillInfo = this.transform.parent.GetComponent<UISkillInfo>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDropItem = Instantiate(Resources.Load<DragDropSkillItem>("UI/SkillItem"), UIManager.Instance.canvas.transform);
        rectTransform = dragDropItem.GetComponent<RectTransform>();
        dragDropItem.SetSkill(skillInfo.skillSO);

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
            SkillCell skillCell = eventData.pointerEnter.GetComponent<SkillCell>();

            if (skillCell.skill != null)
            {
                skillCell.RemoveSkill();
            }

            skillCell.SetSkill(dragDropItem);

            rectTransform.position = eventData.pointerEnter.transform.position;
        }
        else if (eventData.pointerEnter != null && eventData.pointerEnter.tag == "SkillItem")
        {
            DragDropSkillItem dragDropSkillItem = eventData.pointerEnter.GetComponent<DragDropSkillItem>();

            dragDropSkillItem.SetSkill(skillInfo.skillSO);

            Destroy(dragDropItem.gameObject);
        }
        else
        {
            Destroy(dragDropItem.gameObject);
        }
    }
}
