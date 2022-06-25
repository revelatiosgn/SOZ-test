using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using SevenBoldPencil.EasyEvents;
using UnityEngine;
using Voody.UniLeo.Lite;

public class Startup : MonoBehaviour
{
    private static Startup _instance;

    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;
    private SharedData _sharedData;

    public static EcsWorld World => _instance._world;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _world = new EcsWorld();

        _sharedData = new SharedData
        {
            EventsBus = new EventsBus()
        };

        _updateSystems = new EcsSystems(_world, _sharedData);
        _updateSystems
            .ConvertScene()
            .Add(new PlayerInputSystem())
            .Add(new PlayerEventSystem())
            .Add(new PlayerMoveSystem())
            .Add(new PlayerInteractSystem())
            .Add(new AnimationSystem())
            .Add(new CombatSystem())
            .Add(new HealthSystem())
            .Add(new MoveSystem())
            .Add(new BTreeSystem())
            .Add(_sharedData.EventsBus.GetDestroyEventsSystem()
                .IncSingleton<PlayerClickEvent>()
                .IncSingleton<PlayerMoveEvent>()
                .IncSingleton<PlayerInteractEvent>())
            .Inject()
            .Init();

        _fixedUpdateSystems = new EcsSystems(_world, _sharedData);
        _fixedUpdateSystems
            .Add(new VisionSystem())
            .Inject()
            .Init();
    }

    private void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems.Run();
    }

    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _world.Destroy();
        _sharedData.EventsBus.Destroy();
    }
}
