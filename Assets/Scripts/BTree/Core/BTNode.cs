using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode : ScriptableObject
{
    public abstract bool Evaluate(BTBlackboard blackboard);
}
