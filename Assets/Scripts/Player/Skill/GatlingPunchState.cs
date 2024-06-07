using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GatlingPunchState : PlayerSkillState
{
    private float waitTime;
    private float punchTime;
    private bool isPunching;

    public float speedIncreaseRate = 0.2f; // 애니메이션 재생 속도 증가율
    public float maxSpeed = 1.8f; // 최대 애니메이션 재생 속도
    private float currentSpeed = 1.1f; // 현재 애니메이션 재생 속도

    private bool alreadyApplyForce;

    private PlayerAttackData attackData;
    private AttackInfoData attackInfoData;

    private Skill skill;

    public GatlingPunchState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        attackData = Resources.Load<PlayerSkillSO>("Skills/GatlingPunch").attackData;
        skill = stateMachine.player.skillController.GetSkill(attackData.attackID);
    }

    public override void EnterState()
    {
        base.EnterState();

        attackInfoData = attackData.AttackInfoDatas[0];

        waitTime = 1f;
        punchTime = 5f;
        isPunching = false;

        currentSpeed = 1.1f;

        stateMachine.player.animationEventReceiver.animationEvent += DamageEnemy;
        stateMachine.player.animationEventReceiver.SetAnimationEvent(attackData.attackID);

        SetAnimationBool(stateMachine.player.animationData.gatlingPunchParameterHash, true);
        stateMachine.player.animator.SetInteger("GatlingPunchCombo", 0);

        alreadyApplyForce = false;
    }

    public override void ExitState()
    {
        base.ExitState();

        InitPunchSpeed();

        stateMachine.player.animationEventReceiver.animationEvent -= DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.gatlingPunchParameterHash, false);
    }

    public void DamageEnemy()
    {
        Vector3 boxCenter = attackInfoData.hitBoxCenterOffset + stateMachine.player.transform.position + stateMachine.player.transform.forward *
            attackInfoData.hitBox.z / 2f;
        Quaternion boxRotation = Quaternion.LookRotation(stateMachine.player.transform.forward);

        stateMachine.player.testGizmo = new Test(true, attackInfoData.hitBox, boxCenter, boxRotation);

        Collider[] colliders = Physics.OverlapBox(boxCenter, attackInfoData.hitBox / 2f, boxRotation);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (collider.transform.TryGetComponent(out DamageReceiver damageReceiver))
                {
                    Vector3 force;
                    EffectManager.Instance.PlayEffect("PunchEffect", boxCenter);

                    if (!damageReceiver.isAirborne)
                    {
                        force = Vector3.zero;
                        damageReceiver.Damage(attackInfoData.damage * skill.GetSkillLevel(), force);
                        damageReceiver.Stagger(0.2f);
                    }
                    else
                    {
                        force = stateMachine.player.transform.forward * 3f + collider.transform.up * 12f;
                        damageReceiver.Damage(attackInfoData.damage * skill.GetSkillLevel(), force);
                    }
                }
            }
        }
    }

    private void TryApplyForce()
    {
        if (alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.player.forceReceiver.ResetForceReceiver();

        // 전진공격
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.force);
        stateMachine.player.forceReceiver.drag = attackInfoData.drag;
    }

    private void PunchSpeedUp()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); 

            stateMachine.player.animator.SetFloat("GatlingPunchSpeed", currentSpeed);
        }
    }

    private void InitPunchSpeed()
    {
        stateMachine.player.animator.SetFloat("GatlingPunchSpeed", 1f);
    }

    public override void UpdateState()
    {
        //ForceMove();

        if (!isPunching)
        {
            waitTime -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                isPunching = true;
                stateMachine.player.animator.SetInteger("GatlingPunchCombo", 1);
            }

            if (waitTime <= 0f)
            {
                SetPostDelay(0.2f);
            }
        }
        else
        {
            punchTime -= Time.deltaTime;

            PunchSpeedUp();

            if (Input.GetMouseButtonUp(0) || (punchTime <= 0f))
            {
                SetPostDelay(0.5f);
            }
        }
    }
}
