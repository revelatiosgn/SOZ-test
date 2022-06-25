using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct AnimationData
{
    public Animator Animator;
    public Queue<AnimationEvent> AnimationEvents;
}

public enum AnimationEvent
{
    AttackExec
}
