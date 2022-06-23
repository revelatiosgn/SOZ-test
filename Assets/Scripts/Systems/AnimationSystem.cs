using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class AnimationSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private readonly EcsFilterInject<Inc<AnimationData, NavAgentData>> _filter = default;
    private readonly EcsPoolInject<AnimationData> _animationPool = default;
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;
            
    private int _moveSpeedHash = Animator.StringToHash("MoveSpeed");

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            ref AnimationData animationData = ref _animationPool.Value.Get(entity);
            ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);

            float moveSpeed = navAgentData.NavMeshAgent.velocity.magnitude / navAgentData.NavMeshAgent.speed;
            animationData.Animator.SetFloat(_moveSpeedHash, moveSpeed);
        }
    }

    public void Init(EcsSystems systems)
    {
        MyEventBus.OnAttackBegin += OnAttackBegin;
    }

    public void Destroy(EcsSystems systems)
    {
        MyEventBus.OnAttackBegin -= OnAttackBegin;
    }

    private void OnAttackBegin(int entity)
    {
        ref AnimationData animationData = ref _animationPool.Value.Get(entity);
        // animationData.Animator.Play("Attack");
        Debug.Log("Attack animation");
        animationData.Animator.Play("Attack");
    }
}
