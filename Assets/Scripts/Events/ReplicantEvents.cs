using SevenBoldPencil.EasyEvents;
using UnityEngine;

public struct AttackBeginEvent : IEventReplicant 
{
    public int AttackerEnity;
}

public struct AttackExecEvent : IEventReplicant 
{
    public int AttackerEnity;
}

public struct AttackDamageEvent : IEventReplicant 
{
    public int DamagedEntity;
    public float Damage;
}

public struct DeathEvent : IEventReplicant 
{
    public int DeadEntity;
}