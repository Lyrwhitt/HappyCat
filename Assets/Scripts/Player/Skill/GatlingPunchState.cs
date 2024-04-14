using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GatlingPunchState : PlayerSkillState
{
    private float waitTime;
    private float punchTime;
    private bool isPunching;

    public float speedIncreaseRate = 0.1f; // �ִϸ��̼� ��� �ӵ� ������
    public float maxSpeed = 1.5f; // �ִ� �ִϸ��̼� ��� �ӵ�
    private float currentSpeed = 1.1f; // ���� �ִϸ��̼� ��� �ӵ�

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
        // �÷��̾��� ��ġ�� ������ �������� �簢�� ����� ������ �����մϴ�.
        Vector3 boxCenter = attackInfoData.hitBoxCenterOffset + stateMachine.player.transform.position + stateMachine.player.transform.forward *
            attackInfoData.hitBox.z / 2f;
        Quaternion boxRotation = Quaternion.LookRotation(stateMachine.player.transform.forward);

        stateMachine.player.testGizmo = new Test(true, attackInfoData.hitBox, boxCenter, boxRotation);

        // OverlapBox �Լ��� ���� �簢�� ����� ���� �ȿ� �ִ� ���� �����մϴ�.
        Collider[] colliders = Physics.OverlapBox(boxCenter, attackInfoData.hitBox / 2f, boxRotation);

        // ������ �浹�� ������Ʈ�� ���� ó���մϴ�.
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

        // ��������
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.force);
        stateMachine.player.forceReceiver.drag = attackInfoData.drag;
    }

    private void PunchSpeedUp()
    {
        // �ִ� �ӵ� ������ ���� �ӵ��� ������Ŵ
        if (currentSpeed < maxSpeed)
        {
            // ���������� �ӵ��� ������Ŵ
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); // �ִ� �ӵ� ����

            // �ִϸ��̼� ��� �ӵ� ����
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
