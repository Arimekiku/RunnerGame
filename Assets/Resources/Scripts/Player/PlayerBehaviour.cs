using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBlock[] _initialBlocks;
    [SerializeField] private Rigidbody _playerModelBody;
    [SerializeField] private Transform _blockHolder;
    [SerializeField] private Vector3 _velocity;
    
    public BlockManager BlockManager { get; private set; }
    public Transform BlockHolder => _blockHolder;
    public Transform PlayerModel => _playerModelBody.transform;

    public event Action OnNewBlock;

    private PlayerAnimator _animator;
    
    public void Init()
    {
        _animator = GetComponent<PlayerAnimator>();
        _animator.Init();
        
        BlockManager = new(_initialBlocks);
        BlockManager.OnLose += _animator.TriggerRagdollPhysics;
        
        _playerModelBody.isKinematic = false;
    }

    public void MoveForward()
    {
        transform.Translate(_velocity * Time.deltaTime);
    }
    
    public void MoveSideways(float xOffset)
    {
        Vector3 newPosition = new(xOffset, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    
    public void OnBlockSpawned(Vector3 newPosition)
    {
        _animator.TriggerJumpAnimation();
        _playerModelBody.transform.position = newPosition;
        
        OnNewBlock?.Invoke();
    }
}