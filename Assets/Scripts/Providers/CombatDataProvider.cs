using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class CombatDataProvider : MonoProvider<CombatData>
{
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackDelay = 3f;
    [SerializeField] private float _attackDamage = 5f;

    private void Awake()
    {
        value.TargetEnitity = -1;
        value.AttackRange = _attackRange;
        value.AttackDelay = _attackDelay;
        value.AttackDamage = _attackDamage;
    }
}
