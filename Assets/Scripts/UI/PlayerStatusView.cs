using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusView : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    public void UpdateHealth(float health, float maxHealth, float percentage)
    {
        hpText.SetText(string.Concat(health.ToString(), "/", maxHealth.ToString()));
        hpSlider.value = percentage;
    }
}
