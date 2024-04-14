using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GatlingPunchState : PlayerSkillState
{
    private float waitTime;
    private float punchTime;
    private bool isPunching;

    public float speedIncreaseRate = 0.1f; // 애니메이션 재생 속도 증가율
    public float maxSpeed = 1.5f; // 최대 애니메이션 재생 속도
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
        // 플레이어의 위치와 방향을 기준으로 사각형 모양의 영역을 생성합니다.
        Vector3 boxCenter = attackInfoData.hitBoxCenterOffset + stateMachine.player.transform.position + stateMachine.player.transform.forward *
            attackInfoData.hitBox.z / 2f;
        Quaternion boxRotation = Quaternion.LookRotation(stateMachine.player.transform.forward);

        stateMachine.player.testGizmo = new Test(true, attackInfoData.hitBox, boxCenter, boxRotation);

        // OverlapBox 함수를 통해 사각형 모양의 영역 안에 있는 적을 검출합니다.
        Collider[] colliders = Physics.OverlapBox(boxCenter, attackInfoData.hitBox / 2f, boxRotation);

        // 각각의 충돌한 오브젝트에 대해 처리합니다.
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (collider.transform.TryGetComponent(out Health health))
                {
                    EffectManager.Instance.PlayEffect("PunchEffect", boxCenter);
                    health.TakeDamage(attackInfoData.damage * skill.GetSkillLevel());
                }

                if (collider.transform.TryGetComponent(out ForceReceiver forceReceiver))
                {
                    //rigidbody.velocity = Vector3.zero;
                    //forceReceiver.AddForce(stateMachine.player.transform.forward * 3f + collider.transform.up * 12f);
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
        // 최대 속도 이하일 때만 속도를 증가시킴
        if (currentSpeed < maxSpeed)
        {
            // 점진적으로 속도를 증가시킴
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); // 최대 속도 제한

            // 애니메이션 재생 속도 설정
            stateMachine.player.animator.SetFloat("GatlingPunchSpeed", currentSpeed);
        }
    }

    private void InitPunchSpeed()
    {
        stateMachine.player.animator.SetFloat("GatlingPunchSpeed", 1f);
    }

    public override void UpdateState()
    {
        ForceMove();

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
                stateMachine.ChangeState(stateMachine.idleState);
            }
        }
        else
        {
            punchTime -= Time.deltaTime;

            PunchSpeedUp();

            if (Input.GetMouseButtonUp(0) || (punchTime <= 0f))
            {
                stateMachine.ChangeState(stateMachine.idleState);
            }
        }
    }
}
