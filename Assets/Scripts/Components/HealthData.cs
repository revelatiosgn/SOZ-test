using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct HealthData
{
    public float MaxHealth;
    public float Health;
    public bool IsDead => Health <= 0f;
}
