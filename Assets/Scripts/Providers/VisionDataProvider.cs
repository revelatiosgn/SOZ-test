using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class VisionDataProvider : MonoProvider<VisionData>
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _angle = 120f;

    private void Awake()
    {
        value.Radius = _radius;
        value.Angle = _angle;
        value.VisibleEntities = new List<int>();
    }
}
