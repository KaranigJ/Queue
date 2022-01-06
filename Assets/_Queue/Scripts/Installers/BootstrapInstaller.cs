using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public MusicService MusicService;
    public SoundService SoundService;
    
    public override void InstallBindings()
    {
        BindSoundService();
        BindMusicService();
    }

    private void BindSoundService()
    {
        Container
            .Bind<ISoundService>()
            .To<SoundService>()
            .FromComponentInNewPrefab(SoundService)
            .AsSingle()
            .NonLazy();
    }
    
    private void BindMusicService()
    {
        Container
            .Bind<IMusicService>()
            .To<MusicService>()
            .FromComponentInNewPrefab(MusicService)
            .AsSingle()
            .NonLazy();
    }

}