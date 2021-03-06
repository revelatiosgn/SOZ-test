using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class AnimationSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<AnimationData>> _filter = default;
    
    private readonly EcsPoolInject<AnimationData> _animationPool = default;
    private readonly EcsPoolInject<NavAgentData> _navAgentPool = default;
            
    private int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _deathHash = Animator.StringToHash("Death");

    public void Run(EcsSystems systems)
    {
        EventsBuffer eventsBuffer = systems.GetShared<SharedData>().EventsBuffer;

        foreach (int entity in _filter.Value)
        {
            ref AnimationData animationData = ref _animationPool.Value.Get(entity);

            // Move
            if (_navAgentPool.Value.Has(entity))
            {
                ref NavAgentData navAgentData = ref _navAgentPool.Value.Get(entity);
                float moveSpeed = navAgentData.NavMeshAgent.velocity.magnitude / navAgentData.NavMeshAgent.speed;
                animationData.Animator.SetFloat(_moveSpeedHash, moveSpeed);
            }

            // Events
            while (animationData.AnimationEvents.TryDequeue(out AnimationEvent animationEvent))
            {
                if (animationEvent == AnimationEvent.AttackExec)
                    eventsBuffer.NewEvent<AttackExecEvent>().AttackerEnity = entity;
            }
        }

        // Attack begin
        foreach (var eventEntity in eventsBuffer.GetEventBodies<AttackBeginEvent>(out var eventsPool))
        {
            int attackerEntity = eventsPool.Get(eventEntity).AttackerEnity;
            ref AnimationData animationData = ref _animationPool.Value.Get(attackerEntity);
            animationData.Animator.CrossFade(_attackHash, .01f);
        }

        // Death
        foreach (var eventEntity in eventsBuffer.GetEventBodies<DeathEvent>(out var eventsPool))
        {
            int deadEntity = eventsPool.Get(eventEntity).DeadEntity;
            ref AnimationData animationData = ref _animationPool.Value.Get(deadEntity);
            animationData.Animator.CrossFade(_deathHash, .01f);
        }
    }
}
