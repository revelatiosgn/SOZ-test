using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo.Lite;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private ConvertToEntity _convertToEntity;

    public void Attack()
    {
        MyEventBus.OnAttackEnd?.Invoke(_convertToEntity.TryGetEntity().Value);
    }
}
