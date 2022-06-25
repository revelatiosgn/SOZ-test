using SevenBoldPencil.EasyEvents;
using UnityEngine;

public struct PlayerClickEvent : IEventSingleton 
{
    public Vector3 MousePosition;
}

public struct PlayerMoveEvent : IEventSingleton 
{
    public Vector3 Destination;
}

public struct PlayerInteractEvent : IEventSingleton 
{
    public int Entity;
}