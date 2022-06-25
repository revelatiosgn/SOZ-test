using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo.Lite;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private ConvertToEntity _convertToEntity;

    public void AttackExec()
    {
        Enqueue(AnimationEvent.AttackExec);
    }

    private void Enqueue(AnimationEvent animationEvent)
    {
        Startup.World
            .GetPool<AnimationData>()
            .Get(_convertToEntity.TryGetEntity().Value)
            .AnimationEvents
            .Enqueue(animationEvent);
    }
}
