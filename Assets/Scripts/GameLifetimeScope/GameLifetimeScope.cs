using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Scene Preferences")]
    [SerializeField] private LevelBuilder LevelBuilder;
    [SerializeField] private UIManager UIManager;
    
    [Header("Prefabs")]
    [SerializeField] private PlayerBehaviour PlayerPrefab;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerInput>(Lifetime.Scoped);
        RegisterAssetProvider(builder);
        RegisterObjectResolver(builder);
        builder.Register<BlockManager>(Lifetime.Singleton);
        builder.RegisterComponentInNewPrefab(PlayerPrefab, Lifetime.Singleton);
        builder.Register<TrackFactory>(Lifetime.Singleton);
        builder.Register<BlockFactory>(Lifetime.Singleton);
        builder.Register<UITextTemplateFactory>(Lifetime.Singleton);
        builder.RegisterComponent(UIManager);
        builder.RegisterComponent(LevelBuilder);
    }

    private void RegisterAssetProvider(IContainerBuilder builder)
    {
        builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
    }

    private void RegisterObjectResolver(IContainerBuilder builder)
    {
        builder.Register<IObjectResolver, Container>(Lifetime.Scoped);
    }
}
