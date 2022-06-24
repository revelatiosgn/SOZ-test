using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class AnimationSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AnimationData, NavAgentData>> _filter = default;
    private readonly EcsPoolInject<AnimationData> _animationPool = default;
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;
            
    private int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _deathHash = Animator.StringToHash("Death");

    public void Run(EcsSystems systems)
    {
        EventsBus eventsBus = systems.GetShared<SharedData>().EventsBus;
        eventsBus.DestroyEvents<AttackExecEvent>();

        // Move
        foreach (int entity in _filter.Value)
        {
            ref AnimationData animationData = ref _animationPool.Value.Get(entity);
            ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);

            float moveSpeed = navAgentData.NavMeshAgent.velocity.magnitude / navAgentData.NavMeshAgent.speed;
            animationData.Animator.SetFloat(_moveSpeedHash, moveSpeed);
        }

        // Attack begin
        foreach (var eventEntity in eventsBus.GetEventBodies<AttackBeginEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEntity).AttackerEnity;
            ref AnimationData animationData = ref _animationPool.Value.Get(attackerEntity);
            animationData.Animator.CrossFade(_attackHash, .01f);

            eventsBus.NewEvent<AttackExecEvent>().AttackerEnity = attackerEntity;
        }

        // Death
        foreach (var eventEntity in eventsBus.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            int deadEntity = eventsPool.Get(eventEntity).DeadEntity;
            ref AnimationData animationData = ref _animationPool.Value.Get(deadEntity);
            animationData.Animator.CrossFade(_deathHash, .01f);
        }
    }
}
