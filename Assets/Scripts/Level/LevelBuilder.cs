using Cinemachine;
using UnityEngine;
using VContainer;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Transform PlayerParent;
    [SerializeField] private LevelInfo LevelInfo;
    [SerializeField] private CinemachineVirtualCamera DefaultCamera;

    private TrackFactory _trackFactory;
    private PlayerBehaviour _player;
    
    [Inject]
    public void Init(TrackFactory trackFactory, PlayerBehaviour playerBehaviour)
    {
        _trackFactory = trackFactory;
        _player = playerBehaviour;
    }

    private void Awake()
    {
        InitPlayer();
        InitTrack();
    }
    
    private void InitPlayer()
    {
        _player.transform.parent = PlayerParent;
        _player.transform.position = PlayerParent.position;
        
        DefaultCamera.Follow = _player.transform;
    }

    private void InitTrack()
    {
        for (int i = 0; i < LevelInfo.TrackSegmentCount; i++)
            _trackFactory.CreateInstance(transform);
    }
}