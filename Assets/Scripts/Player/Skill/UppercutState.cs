using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutState : PlayerSkillState
{
    private bool alreadyApplyForce;

    private PlayerAttackData attackData;
    private AttackInfoData attackInfoData;

    private Skill skill;

    public UppercutState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        attackData = Resources.Load<PlayerSkillSO>("Skills/Uppercut").attackData;
        attackInfoData = attackData.AttackInfoDatas[0];
        //skillLevel = stateMachine.player.skillController.skillLevelData[attackData.attackID];
        skill = stateMachine.player.skillController.GetSkill(attackData.attackID);
    }

    public override void EnterState()
    {
        base.EnterState();

        alreadyApplyForce = false;

        stateMachine.player.animationEventReceiver.animationEvent += DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.uppercutParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.player.animationEventReceiver.animationEvent -= DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.uppercutParameterHash, false);
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
                    forceReceiver.AddForce(stateMachine.player.transform.forward * 3f + collider.transform.up * 12f);
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

    public override void UpdateState()
    {
        //base.UpdateState();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Uppercut");

        if (normalizedTime < 1f)
        {
            // ������ Ʈ������ Ÿ���� ���� �� ��, �޺� ����
            if (normalizedTime >= attackInfoData.forceTransitionTime)
                TryApplyForce();
        }
        // ��� �ٺ��� ����
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}