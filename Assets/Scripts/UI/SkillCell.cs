using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    [HideInInspector] public DragDropSkillItem skill;

    public TextMeshProUGUI cooldownText;
    private float cooldown = 0f;
    private GameObject cooldownObj;
    private Image cooldownImg;

    public bool skillUsable = true;

    private void Start()
    {
        cooldownObj = cooldownText.transform.parent.gameObject;
        cooldownObj.SetActive(false);
        cooldownImg = cooldownObj.GetComponent<Image>();
    }

    public DragDropSkillItem GetSkill()
    {
        return skill;
    }

    public void SetCooldown(float cooldown)
    {
        if(skillUsable)
            StartCoroutine(SkillCooldown(cooldown));
    }

    private IEnumerator SkillCooldown(float cooldown)
    {
        skillUsable = false;
        this.cooldown = cooldown;
        cooldownObj.SetActive(true);

        while (this.cooldown > 0f)
        {
            this.cooldown -= Time.deltaTime;

            cooldownText.SetText(Mathf.FloorToInt(this.cooldown + 1).ToString());
            cooldownImg.fillAmount = this.cooldown / cooldown;

            yield return null;
        }

        skillUsable = true;
        cooldownObj.SetActive(false);
    }

    public void SetSkill(DragDropSkillItem skillItem)
    {
        skill = skillItem;
        skillItem.canvasGroup.blocksRaycasts = true;
        skill.transform.SetParent(transform.GetChild(0), false);
    }

    public void RemoveSkill()
    {
        Destroy(skill.gameObject);
        skill = null;
    }
}
