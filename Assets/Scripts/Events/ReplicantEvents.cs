using SevenBoldPencil.EasyEvents;
using UnityEngine;

public struct PlayerClickEvent : IEventReplicant 
{
    public Vector3 MousePosition;
}

public struct PlayerMoveEvent : IEventReplicant 
{
    public Vector3 Destination;
}

public struct PlayerInteractEvent : IEventReplicant 
{
    public int Entity;
}

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