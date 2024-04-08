using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSkillItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public PlayerSkillSO skillSO;

    private RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;

    private DragDropSkillItem dragDropItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDropItem = Instantiate(Resources.Load<DragDropSkillItem>("UI/SkillItem"), UIManager.Instance.canvas.transform);
        rectTransform = dragDropItem.GetComponent<RectTransform>();
        dragDropItem.skillSO = skillSO;

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

            if(skillCell.skill != null)
            {
                skillCell.RemoveSkill();
            }

            skillCell.SetSkill(dragDropItem);

            dragDropItem.canvasGroup.blocksRaycasts = true;
            rectTransform.position = eventData.pointerEnter.transform.position;
        }
        else
        {
            Destroy(dragDropItem.gameObject);
        }
    }
}
