using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Health
{
    public PlayerStatusView statusView;

    protected override void Start()
    {
        base.Start();

        AddEvent();
        statusView.UpdateHealth(health, statusData.maxHP, GetPercentageHP());
    }

    private void AddEvent()
    {
        onDie += OnDie;
        onHealthChange += OnHealthChange;
    }

    private void OnHealthChange()
    {
        statusView.UpdateHealth(health, statusData.maxHP, GetPercentageHP());
    }

    private void OnDie()
    {
        Debug.Log("�ֱऱ");
    }
}
