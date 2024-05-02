using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    private Slime slime;

    private Transform player;
    private BTNode root;

    public float waitTime = 0f;

    private float lastAttackTime;
    private float attackCooldown = 2f;

    public float detectDistance = 10f;
    public float attackDistance = 2f;
    public float moveSpeed = 2f;

    private Vector3 roamDestination; // 돌아다닐 방향
    private float roamRange = 3f; // 돌아다닐 범위
    private bool roaming = false; // Roam 중인지 여부를 나타내는 플래그

    private void Start()
    {
        slime = this.GetComponent<Slime>();

        player = GameManager.Instance.player.transform;

        SelectorNode selector = new SelectorNode();

        SequenceNode damageSequence = new SequenceNode();
        ConditionNode damageCondition = new ConditionNode(DamageCondition);
        ActionNode damageAction = new ActionNode(DamageAction);
        damageSequence.AddChild(damageCondition);
        damageSequence.AddChild(damageAction);

        SequenceNode waitSequence = new SequenceNode();
        ConditionNode checkWaitTime = new ConditionNode(CheckWaitTime);
        ActionNode waitForNextAction = new ActionNode(WaitForNextAction);
        waitSequence.AddChild(checkWaitTime);
        waitSequence.AddChild(waitForNextAction);

        SequenceNode attackSequence = new SequenceNode();
        ConditionNode checkRange = new ConditionNode(CheckAttackRange);
        ActionNode attackPlayer = new ActionNode(AttackPlayer);
        attackSequence.AddChild(checkRange);
        attackSequence.AddChild(attackPlayer);

        SequenceNode detectSequence = new SequenceNode();
        ConditionNode detectPlayer = new ConditionNode(DetectPlayer);
        ActionNode moveTowardsPlayer = new ActionNode(MoveToPlayer);
        detectSequence.AddChild(detectPlayer);
        detectSequence.AddChild(moveTowardsPlayer);

        SequenceNode roamSequence = new SequenceNode();
        ConditionNode roamCondition = new ConditionNode(CheckRoamCondition);
        ActionNode roamAction = new ActionNode(Roam);
        roamSequence.AddChild(roamCondition);
        roamSequence.AddChild(roamAction);

        selector.AddChild(damageSequence);
        selector.AddChild(waitSequence);
        selector.AddChild(attackSequence);
        selector.AddChild(detectSequence);
        selector.AddChild(roamSequence);

        root = selector;
    }

    private void Update()
    {
        root.Evaluate();
    }

    #region Damage Sequence
    private bool DamageCondition()
    {
        if (slime.damageReceiver.isStagger || slime.damageReceiver.isAirborne)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private NodeState DamageAction()
    {
        return NodeState.Success;
    }
    #endregion

    #region Wait Sequence
    private NodeState WaitForNextAction()
    {
        slime.animator.SetFloat(slime.animationData.speedParameterHash, 0f);

        waitTime -= Time.deltaTime;

        if(waitTime <= 0f)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Running;
        }
    }

    private bool CheckWaitTime()
    {
        if(waitTime > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    private bool CheckRoamCondition()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private NodeState Roam()
    {
        if(roaming && Vector3.Distance(transform.position, roamDestination) < 0.1f)
        {
            waitTime = Random.Range(1f, 3f);
            roaming = false;

            return NodeState.Success;
        }

        if (!roaming)
        {
            slime.animator.SetFloat(slime.animationData.speedParameterHash, 1f);
            roamDestination = GetRandomRoamDestination();
            roaming = true;
        }

        roamDestination.y = transform.position.y;

        // 적을 랜덤한 방향으로 이동시킵니다.
        transform.LookAt(roamDestination);
        slime.controller.Move(transform.forward * Time.deltaTime * 1.5f);

        return NodeState.Running;
    }

    // 랜덤한 Roam 목적지 계산
    private Vector3 GetRandomRoamDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRange, NavMesh.AllAreas);

        Debug.Log(hit.position);

        return hit.position;
    }

    #region Attack Sequence
    private bool CheckAttackRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private NodeState AttackPlayer()
    {
        if ((Time.time - lastAttackTime) >= attackCooldown)
        {
            Debug.Log("Attacking Player!");
            lastAttackTime = Time.time; // 마지막 공격 시간 업데이트

            this.transform.LookAt(player);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);

            slime.animator.SetTrigger(slime.animationData.attackParameterHash);

            return NodeState.Success; // 공격 성공
        }
        else
        {
            return NodeState.Running; // 공격 대기 중
        }
    }
    #endregion

    #region Detect Sequence
    private bool DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= detectDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private NodeState MoveToPlayer()
    {
        slime.animator.SetFloat(slime.animationData.speedParameterHash, 1f);

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = targetRotation;

        slime.controller.Move(direction * Time.deltaTime * moveSpeed);

        return NodeState.Success;
    }
    #endregion
}
