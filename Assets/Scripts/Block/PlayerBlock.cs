using System;
using System.Linq;
using UnityEngine;
using VContainer;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private float MaxRaycastDistance;
    [SerializeField] private float SizeShrinkMultiplier;
    [SerializeField] private BoxCollider Collider;
    
    public event Action OnCollideWithEnemy;

    public bool IsStuck => transform.parent == null;

    private RaycastHit _boxHitInfo;
    private TrailRenderer _trail;
    private bool _boxHitDetected;
    private Rigidbody _body;

    [Inject]
    public void Init(BlockManager blockManager, PlayerBehaviour playerBehaviour)
    {
        blockManager.AddBlock(this);
        
        OnCollideWithEnemy += blockManager.RemoveBlock;
        
        playerBehaviour.OnBlockSpawned(transform.position + Vector3.up);
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        CheckForAnyEnemyBlock();
    }

    private void CheckForAnyEnemyBlock()
    {
        Vector3 boxSize = transform.localScale / SizeShrinkMultiplier;

        Collider[] colliders = Physics.OverlapBox(Collider.bounds.center + Vector3.forward * MaxRaycastDistance, boxSize, transform.rotation);
        _boxHitDetected = colliders != null;
        
        if (!_boxHitDetected)
            return;

        if (!colliders.Any(coll => coll.TryGetComponent(out EnemyBlock _))) 
            return;
        
        transform.SetParent(null);
        OnCollideWithEnemy?.Invoke();
        _body.isKinematic = true;
    }

    public void EnableTrail()
    {
        _trail.enabled = true;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_boxHitDetected)
        {
            Gizmos.DrawRay(Collider.bounds.center, transform.forward * _boxHitInfo.distance);
            Gizmos.DrawWireCube(Collider.bounds.center + transform.forward * _boxHitInfo.distance, transform.localScale / SizeShrinkMultiplier);
        }
        else
        {
            Gizmos.DrawRay(Collider.bounds.center, transform.forward * MaxRaycastDistance);
            Gizmos.DrawWireCube(Collider.bounds.center + transform.forward * MaxRaycastDistance, transform.localScale / SizeShrinkMultiplier);
        }
    }
#endif
}