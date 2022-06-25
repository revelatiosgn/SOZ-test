using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class FactionDataProvider : MonoProvider<FactionData>
{
    [SerializeField] private Faction _faction;

    private void Awake()
    {
        value.Faction = _faction;
    }
}
