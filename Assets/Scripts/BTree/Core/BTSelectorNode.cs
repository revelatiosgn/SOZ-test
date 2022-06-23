using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectorNode", menuName = "BTree/Selector")]
public class BTSelectorNode : BTNode
{
    [SerializeField] private List<BTNode> _nodes;

    public override bool Evaluate(BTBlackboard blackboard)
    {
        foreach (BTNode node in _nodes)
        {
            if (node.Evaluate(blackboard))
                return true;
        }

        return false;
    }
}
