using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";
    [SerializeField] private string _speedParameterName = "Speed";
    [SerializeField] private string _dashParameterName = "Dash";

    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParameterName = "Jump";
    [SerializeField] private string _fallParameterName = "Fall";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    public int groundParameterHash { get; private set; }
    public int idleParameterHash { get; private set; }
    public int walkParameterHash { get; private set; }
    public int runParameterHash { get; private set; }
    public int speedParameterHash { get; private set; }
    public int dashParameterHash { get; private set; }

    public int airParameterHash { get; private set; }
    public int jumpParameterHash { get; private set; }
    public int fallParameterHash { get; private set; }

    public int attackParameterHash { get; private set; }
    public int comboAttackParameterHash { get; private set; }

    public void Initialize()
    {
        groundParameterHash = Animator.StringToHash(_groundParameterName);
        idleParameterHash = Animator.StringToHash(_idleParameterName);
        walkParameterHash = Animator.StringToHash(_walkParameterName);
        runParameterHash = Animator.StringToHash(_runParameterName);
        speedParameterHash = Animator.StringToHash(_speedParameterName);
        dashParameterHash = Animator.StringToHash(_dashParameterName);

        airParameterHash = Animator.StringToHash(_airParameterName);
        jumpParameterHash = Animator.StringToHash(_jumpParameterName);
        fallParameterHash = Animator.StringToHash(_fallParameterName);

        attackParameterHash = Animator.StringToHash(_attackParameterName);
        comboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);
    }
}
