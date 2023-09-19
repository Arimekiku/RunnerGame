using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager
{
    private readonly List<PlayerBlock> _blocks = new();

    public event Action OnLose;
    
    public void AddBlock(PlayerBlock newBlock)
    {
        PlayerBlock lastBlock = _blocks.Count != 0 ? _blocks[^1] : newBlock;

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