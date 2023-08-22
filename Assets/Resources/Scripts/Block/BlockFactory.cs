using UnityEngine;

public class BlockFactory
{
    private readonly Transform _blockParent;
    private readonly PlayerBlock _blockPrefab;
    private readonly PlayerBehaviour _playerInstance;

    public BlockFactory(PlayerBlock blockPrefab, PlayerBehaviour player)
    {
        _blockPrefab = blockPrefab;
        _blockParent = player.BlockHolder;
        _playerInstance = player;
    }

    public void SpawnBlock()
    {
        PlayerBlock newBlock = Object.Instantiate(_blockPrefab, _blockParent);

        newBlock.OnCollideWithEnemy += _playerInstance.BlockManager.RemoveBlock;
        
        _playerInstance.BlockManager.AddBlock(newBlock);
        
        Vector3 newPosition = newBlock.transform.position + Vector3.up;
        _playerInstance.OnBlockSpawned(newPosition);
    }
}