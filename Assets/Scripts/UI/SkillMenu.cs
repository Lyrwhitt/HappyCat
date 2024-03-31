using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{
    public Button closeBtn;

    private void Start()
    {
        this.gameObject.SetActive(false);

        closeBtn.onClick.AddListener(OpenSkillMenu);
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
