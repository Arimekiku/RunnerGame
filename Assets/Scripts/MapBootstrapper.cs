using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class MapBootstrapper : MonoBehaviour
{
    [Header("Player Preferences")]
    [SerializeField] private Transform PlayerSpawnPosition;
    [SerializeField] private PlayerBehaviour PlayerPrefab;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [Header("UI Preferences")] 
    [SerializeField] private Canvas TextPrefab;
    [SerializeField] private UIManager UIManager;

    [Header("Block Preferences")] 
    [SerializeField] private PlayerBlock _blockPrefab;

    [Header("Track Preferences")]
    [SerializeField] private int InitialTrackObjectCount;
    [SerializeField] private Track[] PossibleTrackInstances;

    private TrackFactory _trackFactory;
    private BlockFactory _blockFactory;
    private UITextTemplateFactory _uiTextTemplateFactory;
    private PlayerBehaviour _playerInstance;
    private PlayerInput _playerInput;
    
    private void Awake()
    {
        InitPlayer();
        InitBlocksFactory();
        InitTextFactory();
        InitTrack();

        UIManager.OnRestartShow += _playerInput.DisableInput;
    }

    private void InitTextFactory()
    {
        _uiTextTemplateFactory = new(_playerInstance.PlayerModel, TextPrefab, Camera.main);
        _playerInstance.OnNewBlock += _uiTextTemplateFactory.SpawnTextTemplate;
    }

    private void InitBlocksFactory()
    {
        _blockFactory = new(_blockPrefab, _playerInstance);
    }

    private void InitPlayer()
    {
        _playerInstance = Instantiate(PlayerPrefab, PlayerSpawnPosition.position, quaternion.identity);
        _playerInstance.Init();

        _playerInput = _playerInstance.GetComponent<PlayerInput>();
        _playerInput.Init(UIManager);
        
        _playerInstance.BlockManager.OnLose += UIManager.ShowRestartSection;

        VirtualCamera.Follow = _playerInstance.transform;
    }

    private void InitTrack()
    {
        _trackFactory = new(PossibleTrackInstances, Vector3.zero, _blockFactory);

        for (int i = 0; i < InitialTrackObjectCount; i++)
            _trackFactory.SpawnNextTrackSegment();
    }
}