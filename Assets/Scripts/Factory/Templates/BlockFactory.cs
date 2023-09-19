using VContainer;

public class BlockFactory : GameInstanceFactory
{
    private const string PlayerBlockPrefabPath = "Prefabs/PlayerBlock";

    private readonly PlayerBehaviour _playerBehaviour;
    
    public BlockFactory(PlayerBehaviour playerBehaviour, IAssetProvider assetProvider, IObjectResolver objectResolver) : base(objectResolver)
    {
        PlayerBlock blockPrefab = assetProvider.GetObjectOfType<PlayerBlock>(PlayerBlockPrefabPath);
        DefaultPrefabsList.Add(blockPrefab);

        _playerBehaviour = playerBehaviour;
    }

    public void CreateInstance()
    {
        CreateInstanceByType<PlayerBlock>(_playerBehaviour.CubeHolder);
    }
}