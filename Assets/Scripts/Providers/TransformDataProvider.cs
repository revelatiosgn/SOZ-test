using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class TransformDataProvider : MonoProvider<TransformData>
{
    [SerializeField] private Transform _transform;

    private void Awake()
    {
        value.Transform = _transform;
    }
}
