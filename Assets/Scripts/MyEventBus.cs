using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using SevenBoldPencil.EasyEvents;

public class MyEventBus
{
    public static Action<int> OnAttackBegin; // entity
    public static Action<int> OnAttackEnd; // entity
}