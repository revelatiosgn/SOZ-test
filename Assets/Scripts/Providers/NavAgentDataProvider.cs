using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class NavAgentDataProvider : MonoProvider<NavAgentData>
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _speed = 1f;

    private void Awake()
    {
        value.NavMeshAgent = _navMeshAgent;
        value.NavMeshAgent.speed = _speed;
    }
}
