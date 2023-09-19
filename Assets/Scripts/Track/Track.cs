using System;
using UnityEngine;
using VContainer;

public class Track : MonoBehaviour
{
    [Header("Blocks Preferences")]
    [SerializeField] private BlockPickup[] LevelPickups;
    
    [Header("Spawn Preferences")]
    [SerializeField] private Transform LevelGround;

    [Header("Camera Shake Trigger")] 
    [SerializeField] private CameraScreenShakeTrigger CameraScreenShakeTrigger;

    public Transform Ground => LevelGround;
    
    public event Action<Transform> OnTrackCompleted;
    
    private Transform _parent;

    [Inject]
    public void Init(LevelBuilder parent, BlockFactory blockFactory)
    {
        _parent = parent.transform;
        
        foreach (BlockPickup blockPickup in LevelPickups)
            blockPickup.OnPickupEvent += blockFactory.CreateInstance;
    }
    
    public void Awake()
    {
        CameraScreenShakeTrigger.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour _))
            OnTrackCompleted?.Invoke(_parent);
    }
}