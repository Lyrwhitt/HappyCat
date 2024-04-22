using System;
using System.Collections.Generic;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class BTNode
{
    protected NodeState state;

    public NodeState State
    {
        get
        {
            return state;
        }
    }

    public abstract NodeState Evaluate();
}

// 행동 노드
public class ActionNode : BTNode
{
    private Func<NodeState> action;

    public ActionNode(Func<NodeState> action)
    {
        this.action = action;
    }

    public override NodeState Evaluate()
    {
        return action();
    }
}

// 조건 노드
public class ConditionNode : BTNode
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override NodeState Evaluate()
    {
        return condition() ? NodeState.Success : NodeState.Failure;
    }
}

// 분기 노드
public class SequenceNode : BTNode
{
    private List<BTNode> children = new List<BTNode>();

    public void AddChild(BTNode node)
    {
        children.Add(node);
    }

    public override NodeState Evaluate()
    {
        foreach (var child in children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Failure:
                    return NodeState.Failure;
                case NodeState.Running:
                    return NodeState.Running;
            }
        }
        return NodeState.Success;
    }
}