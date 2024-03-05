using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Health
{

    private void Start()
    {
        AddEvent();
    }

    private void AddEvent()
    {
        onDie += OnDie;
    }

    private void OnDie()
    {
        Debug.Log("�ֱऱ");
    }
}
