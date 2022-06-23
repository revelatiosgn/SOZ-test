using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BTree", menuName = "BTree/Tree")]
public class BTree : BTNode
{
    [SerializeField] private BTNode _node;

    public override bool Evaluate(BTBlackboard blackboard)
    {
        return _node.Evaluate(blackboard);
    }
}
