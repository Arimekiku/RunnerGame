using UnityEngine;

public class BlockFactory : GameInstanceFactory
{
    private const string PlayerBlockPrefabPath = "Prefabs/PlayerBlock";
    
    private readonly PlayerBehaviour _playerInstance;

    public BlockFactory(PlayerBehaviour player) : base(player.transform)
    {
        PlayerBlock blockPrefab = Resources.Load<PlayerBlock>(PlayerBlockPrefabPath);
        DefaultPrefabsList.Add(blockPrefab);
        
        _playerInstance = player;
    }

    public void CreateInstance()
    {
        PlayerBlock newBlock = CreateInstanceByType<PlayerBlock>();

        newBlock.OnCollideWithEnemy += _playerInstance.BlockManager.RemoveBlock;
        
        _playerInstance.BlockManager.AddBlock(newBlock);
        
        Vector3 newPosition = newBlock.transform.position + Vector3.up;
        _playerInstance.OnBlockSpawned(newPosition);
    }
}