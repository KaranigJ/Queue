using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public LevelStateService LevelStateService;
    public LevelUIService LevelUIService;

    public QueueService QueueService;
    public ConsumerSpawnService ConsumerSpawnService;
    public PlayerService PlayerService;

    public EnvironmentService EnvironmentService;

    public override void InstallBindings()
    {
        //Level
        BindLevelUIService();
        BindLevelStateService();

        //Queue
        BindQueueService();
        BindConsumerGraphicSpawner();
        
        BindPlayerService();
        BindEnvironmentService();
    }

    private void BindLevelUIService()
    {
        Container
            .Bind<ILevelUIService>()
            .To<LevelUIService>()
            .FromComponentInNewPrefab(LevelUIService)
            .AsSingle()
            .NonLazy();
    }
    
    private void BindLevelStateService()
    {
        Container
            .Bind<ILevelStateService>()
            .To<LevelStateService>()
            .FromComponentInNewPrefab(LevelStateService)
            .AsSingle()
            .NonLazy();
    }

    private void BindQueueService()
    {
        Container
            .Bind<IQueueService>()
            .To<QueueService>()
            .FromComponentInNewPrefab(QueueService)
            .AsSingle()
            .NonLazy();
    }

    private void BindConsumerGraphicSpawner()
    {
        Container
            .Bind<IConsumerSpawnService>()
            .To<ConsumerSpawnService>()
            .FromComponentInNewPrefab(ConsumerSpawnService)
            .AsSingle()
            .NonLazy();
    }

    private void BindPlayerService()
    {
        Container
            .Bind<IPlayerService>()
            .To<PlayerService>()
            .FromComponentInNewPrefab(PlayerService)
            .AsSingle()
            .NonLazy();
    }

    private void BindEnvironmentService()
    {
        Container
            .Bind<IEnvironmentService>()
            .To<EnvironmentService>()
            .FromComponentInNewPrefab(EnvironmentService)
            .AsSingle()
            .NonLazy();
    }

}
