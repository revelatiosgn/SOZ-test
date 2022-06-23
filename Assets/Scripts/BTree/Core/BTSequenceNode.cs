using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SequenceNode", menuName = "BTree/Sequence")]
public class BTSequenceNode : BTNode
{
    [SerializeField] private List<BTNode> _nodes;

    public override bool Evaluate(BTBlackboard blackboard)
    {
        foreach (BTNode node in _nodes)
        {
            if (!node.Evaluate(blackboard))
                return false;
        }

        return true;
    }
}
