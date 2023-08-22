using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [Header("Player Preferences")]
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private PlayerBehaviour _playerPrefab;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [Header("UI Preferences")] 
    [SerializeField] private Canvas _textPrefab;
    [SerializeField] private UIManager _uiManager;

    [Header("Block Preferences")] 
    [SerializeField] private PlayerBlock _blockPrefab;

    [Header("Track Preferences")]
    [SerializeField] private int _initialTrackObjectCount;
    [SerializeField] private Track[] _possibleTrackInstances;

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

        _uiManager.OnRestartShow += _playerInput.DisableInput;
    }

    private void InitTextFactory()
    {
        _uiTextTemplateFactory = new(_playerInstance.PlayerModel, _textPrefab, Camera.main);
        _playerInstance.OnNewBlock += _uiTextTemplateFactory.SpawnTextTemplate;
    }

    private void InitBlocksFactory()
    {
        _blockFactory = new(_blockPrefab, _playerInstance);
    }

    private void InitPlayer()
    {
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPosition.position, quaternion.identity);
        _playerInstance.Init();

        _playerInput = _playerInstance.GetComponent<PlayerInput>();
        _playerInput.Init(_uiManager);
        
        _playerInstance.BlockManager.OnLose += _uiManager.ShowRestartSection;

        _virtualCamera.Follow = _playerInstance.transform;
    }

    private void InitTrack()
    {
        _trackFactory = new(_possibleTrackInstances, Vector3.zero, _blockFactory);

        for (int i = 0; i < _initialTrackObjectCount; i++)
            _trackFactory.SpawnNextTrackSegment();
    }
}