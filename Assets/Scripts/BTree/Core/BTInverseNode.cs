using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InverseNode", menuName = "BTree/Inverse")]
public class BTInverseNode : BTNode
{
    [SerializeField] private BTNode _node;

    public override bool Evaluate(BTBlackboard blackboard)
    {
        return !_node.Evaluate(blackboard);
    }
}
