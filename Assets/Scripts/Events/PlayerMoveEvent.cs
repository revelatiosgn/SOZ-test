using SevenBoldPencil.EasyEvents;
using UnityEngine;

public struct PlayerMoveEvent : IEventSingleton 
{
    public Vector3 Destination;
}
