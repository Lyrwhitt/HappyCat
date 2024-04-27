using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlimeAnimationData
{
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _speedParameterName = "Speed";
    [SerializeField] private string _attackParameterName = "Attack";

    public int idleParameterHash { get; private set; }
    public int speedParameterHash { get; private set; }
    public int attackParameterHash { get; private set; }

    public void Initialize()
    {
        idleParameterHash = Animator.StringToHash(_idleParameterName);
        speedParameterHash = Animator.StringToHash(_speedParameterName);
        attackParameterHash = Animator.StringToHash(_attackParameterName);
    }
}
