using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    private Slime slime;

    private Transform player;
    private BTNode root;

    private void Start()
    {
        slime = this.GetComponent<Slime>();

        player = GameManager.Instance.player.transform;

        SelectorNode selector = new SelectorNode();

        SequenceNode attackSequence = new SequenceNode();
        ConditionNode checkRange = new ConditionNode(CheckRange);
        ActionNode attackPlayer = new ActionNode(AttackPlayer);
        attackSequence.AddChild(checkRange);
        attackSequence.AddChild(attackPlayer);

        SequenceNode detectSequence = new SequenceNode();
        ConditionNode detectPlayer = new ConditionNode(DetectPlayer);
        ActionNode moveTowardsPlayer = new ActionNode(MoveToPlayer);
        detectSequence.AddChild(detectPlayer);
        detectSequence.AddChild(moveTowardsPlayer);

        selector.AddChild(attackSequence);
        selector.AddChild(detectSequence);

        root = selector;
    }

    private void Update()
    {
        root.Evaluate();
    }

    private bool CheckRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        return distance < 5f;
    }

    private bool DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        return distance > 5f;
    }

    private NodeState MoveToPlayer()
    {
        this.transform.LookAt(player);
        this.transform.Translate(Vector3.forward * Time.deltaTime * 3f);

        return NodeState.Success;
    }

    private NodeState AttackPlayer()
    {
        Debug.Log("Attack Player");

        return NodeState.Success;
    }
}
