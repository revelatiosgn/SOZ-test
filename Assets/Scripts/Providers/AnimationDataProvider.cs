using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Voody.UniLeo.Lite;

public sealed class AnimationDataProvider : MonoProvider<AnimationData>
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        value.Animator = _animator;
    }
}
