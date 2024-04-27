using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeAI : MonoBehaviour
{
    private Slime slime;

    private Transform player;
    private BTNode root;

    private float lastAttackTime;
    private float attackCooldown = 2f;

    public float detectDistance = 10f;
    public float attackDistance = 2f;
    public float moveSpeed = 2f;

    private Vector3 roamDestination; // ���ƴٴ� ����
    private float roamRange = 10f; // ���ƴٴ� ����
    private bool roaming = false; // Roam ������ ���θ� ��Ÿ���� �÷���

    private void Start()
    {
        slime = this.GetComponent<Slime>();

        player = GameManager.Instance.player.transform;

        SelectorNode selector = new SelectorNode();

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

    #region WaitSequence
    private NodeState WaitForNextAction()
    {
        slime.animator.SetFloat(slime.animationData.speedParameterHash, 0f);

        return NodeState.Success;
    }

    private bool CheckWaitTime()
    {
        if((Time.time - lastAttackTime) < attackCooldown)
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
        if (!roaming || (roaming && Vector3.Distance(transform.position, roamDestination) < 0.1f))
        {
            roamDestination = GetRandomRoamDestination();
            roaming = true;
        }

        roamDestination.y = transform.position.y;

        // ���� ������ �������� �̵���ŵ�ϴ�.
        transform.LookAt(roamDestination);
        slime.controller.Move(transform.forward * Time.deltaTime * 1.5f);

        return NodeState.Running;
    }

    // ������ Roam ������ ���
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
            lastAttackTime = Time.time; // ������ ���� �ð� ������Ʈ

            this.transform.LookAt(player);
            slime.animator.SetTrigger(slime.animationData.attackParameterHash);

            return NodeState.Success; // ���� ����
        }
        else
        {
            return NodeState.Running; // ���� ��� ��
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
