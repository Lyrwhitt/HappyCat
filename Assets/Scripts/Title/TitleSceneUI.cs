using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneUI : MonoBehaviour
{
    public Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(StartBtnClicked);
    }

    private void StartBtnClicked()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
}
