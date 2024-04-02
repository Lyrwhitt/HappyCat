using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{
    public Button closeBtn;
    public Transform content;

    public UISkillInfo skillPrefab;

    private void Start()
    {
        this.gameObject.SetActive(false);

        closeBtn.onClick.AddListener(OpenSkillMenu);

        SetPlayerSkillData();
    }

    public void SetPlayerSkillData()
    {
        for(int i = 0; i < GameManager.Instance.player.skillDatas.Length; i++)
        {
            UISkillInfo skill = Instantiate(skillPrefab, content);
            skill.SetSkillData(GameManager.Instance.player.skillDatas[i]);
        }
    }

    public void OpenSkillMenu()
    {
        if (!this.gameObject.activeSelf)
        {
            GameManager.Instance.ChangeCursorLockMode(CursorLockMode.None);
            this.gameObject.SetActive(true);
        }
        else
        {
            GameManager.Instance.ChangeCursorLockMode(CursorLockMode.Locked);
            this.gameObject.SetActive(false);
        }
    }
}
