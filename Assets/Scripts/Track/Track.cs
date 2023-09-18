using System;
using System.Collections;
using UnityEngine;

public class Track : MonoBehaviour
{
    [Header("Blocks Preferences")]
    [SerializeField] private BlockPickup[] _pickups;
    
    [Header("Spawn Preferences")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private AnimationCurve _spawnAnimationCurve;
    [SerializeField] private Transform _trackGround;

    [Header("Camera Shake Trigger")] 
    [SerializeField] private CameraScreenShakeTrigger _cameraScreenShakeTrigger;

    public Transform TrackGround => _trackGround;
    public BlockPickup[] Pickups => _pickups;
    
    public event Action<bool> OnTrackCompleted;
    
    private Vector3 _targetPosition;
    
    public void Init()
    {
        _targetPosition = transform.position;

        _cameraScreenShakeTrigger.Init();
    }

    public IEnumerator SpawnAnimation()
    {
        transform.position += _offset;
        float currentTime = 0f;

        while (transform.position != _targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _spawnAnimationCurve.Evaluate(currentTime));
            yield return new WaitForEndOfFrame();

            currentTime += Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour _))
            OnTrackCompleted?.Invoke(true);
    }
}