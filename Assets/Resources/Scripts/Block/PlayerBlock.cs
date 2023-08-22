using System;
using System.Linq;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private float _maxRaycastDistance;
    [SerializeField] private float _sizeShrinkMultiplier;
    [SerializeField] private BoxCollider _collider;
    
    public event Action OnCollideWithEnemy;

    public bool IsStuck => transform.parent == null;

    private RaycastHit _boxHitInfo;
    private TrailRenderer _trail;
    private bool _boxHitDetected;
    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        CheckForBoxCast();
    }

    private void CheckForBoxCast()
    {
        Vector3 boxSize = transform.localScale / _sizeShrinkMultiplier;

        Collider[] colliders = Physics.OverlapBox(_collider.bounds.center + Vector3.forward * _maxRaycastDistance, boxSize, transform.rotation);
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
            Gizmos.DrawRay(_collider.bounds.center, transform.forward * _boxHitInfo.distance);
            Gizmos.DrawWireCube(_collider.bounds.center + transform.forward * _boxHitInfo.distance, transform.localScale / _sizeShrinkMultiplier);
        }
        else
        {
            Gizmos.DrawRay(_collider.bounds.center, transform.forward * _maxRaycastDistance);
            Gizmos.DrawWireCube(_collider.bounds.center + transform.forward * _maxRaycastDistance, transform.localScale / _sizeShrinkMultiplier);
        }
    }
#endif
}