using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager
{
    private readonly List<PlayerBlock> _blocks;

    public event Action OnLose; 

    public BlockManager(PlayerBlock[] initialBlocks)
    {
        _blocks = new(initialBlocks);
        _blocks.ForEach(b => b.OnCollideWithEnemy += RemoveBlock);
    }
    
    public void AddBlock(PlayerBlock newBlock)
    {
        PlayerBlock lastBlock = _blocks[^1];

        newBlock.transform.position = lastBlock.transform.position + Vector3.up;
        _blocks.Add(newBlock);
    }

    public void RemoveBlock()
    {
        PlayerBlock blockToRemove = _blocks.Find(b => b.IsStuck);
        
        _blocks.Remove(blockToRemove);
        
        if (!CheckForLose())
            _blocks[0].EnableTrail();
    }

    private bool CheckForLose()
    {
        if (_blocks.Count == 0)
        {
            OnLose?.Invoke();
            return true;
        }

        return false;
    }
}