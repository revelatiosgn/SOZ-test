using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class NavAgentDataProvider : MonoProvider<NavAgentData>
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        value.NavMeshAgent = _navMeshAgent;
    }
}
