using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Voody.UniLeo.Lite;

public class VisionSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<VisionData, TransformData>> _filter = default;
    private readonly EcsPoolInject<VisionData> _visionPool = default;
    private readonly EcsPoolInject<TransformData> _transformPool = default;

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            ref VisionData visionData = ref _visionPool.Value.Get(entity);
            ref TransformData transformData = ref _transformPool.Value.Get(entity);

            visionData.VisibleEntities.Clear();

            Collider[] colliders = Physics.OverlapSphere(transformData.Transform.position, visionData.Radius, LayerMask.GetMask("Character"), QueryTriggerInteraction.Collide);
            foreach (Collider collider in colliders)
            {
                if (transformData.Transform != collider.transform)
                {
                    float angle = Vector3.Angle(transformData.Transform.forward, collider.transform.position - transformData.Transform.position);
                    if (angle > visionData.Angle * .5f)
                        continue;

                    // TODO: move to data
                    Vector3 offset = new Vector3(0f, 1f, 0f);
                    if (!Physics.Linecast(transformData.Transform.position + offset, collider.transform.position + offset, LayerMask.GetMask("Environment", "Ground"), QueryTriggerInteraction.Collide))
                    {
                        if (collider.transform.TryGetComponent<ConvertToEntity>(out ConvertToEntity convertToEntity))
                            visionData.VisibleEntities.Add(convertToEntity.TryGetEntity().Value);
                    }
                }
            }
        }
    }
}
