using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropSkillItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public PlayerSkillSO skillSO;

    private RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;
    private Image skillIcon;

    private DragDropSkillItem dragDropItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        skillIcon = GetComponent<Image>();
    }

    public void SetSkill(PlayerSkillSO skillData)
    {
        skillSO = skillData;
        skillIcon.sprite = skillData.attackData.attackImg;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDropItem = Instantiate(Resources.Load<DragDropSkillItem>("UI/SkillItem"), UIManager.Instance.canvas.transform);
        rectTransform = dragDropItem.GetComponent<RectTransform>();
        dragDropItem.SetSkill(skillSO);

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

            rectTransform.position = eventData.pointerEnter.transform.position;
        }
        else if(eventData.pointerEnter != null && eventData.pointerEnter.tag == "SkillItem")
        {
            DragDropSkillItem dragDropSkillItem = eventData.pointerEnter.GetComponent<DragDropSkillItem>();

            dragDropSkillItem.SetSkill(skillSO);

            Destroy(dragDropItem.gameObject);
        }
        else
        {
            Destroy(dragDropItem.gameObject);
            Destroy(this.gameObject);
        }
    }
}
