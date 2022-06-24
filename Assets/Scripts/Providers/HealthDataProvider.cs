using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class HealthDataProvider : MonoProvider<HealthData>
{
    [SerializeField] private float _maxHealth = 100f;

    private void Awake()
    {
        value.MaxHealth = _maxHealth;
        value.Health = value.MaxHealth;
    }
}
