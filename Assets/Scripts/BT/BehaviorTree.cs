using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class BTNode
{
    public abstract NodeState Evaluate();
}

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

public class SelectorNode : BTNode
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
                case NodeState.Success:
                    return NodeState.Success;
                case NodeState.Running:
                    return NodeState.Running;
            }
        }
        return NodeState.Failure;
    }
}