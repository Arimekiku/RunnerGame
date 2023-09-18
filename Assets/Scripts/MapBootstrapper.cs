using Cinemachine;
using UnityEngine;

public class MapBootstrapper : MonoBehaviour
{
    [Header("Player Preferences")]
    [SerializeField] private Transform PlayerParent;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    [Header("UI Preferences")] 
    [SerializeField] private Canvas TextPrefab;
    [SerializeField] private UIManager UIManager;

    [Header("Track Preferences")]
    [SerializeField] private int InitialTrackObjectCount;
    [SerializeField] private Transform TrackParent;

    private TrackFactory _trackFactory;
    private BlockFactory _blockFactory;
    private PlayerFactory _playerFactory;
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
        _blockFactory = new(_playerInstance);
    }

    private void InitPlayer()
    {
        _playerFactory = new(PlayerParent);
        
        _playerInstance = _playerFactory.CreateInstance();

        _playerInput = _playerInstance.GetComponent<PlayerInput>();
        _playerInput.Init(UIManager);
        
        _playerInstance.BlockManager.OnLose += UIManager.ShowRestartSection;

        VirtualCamera.Follow = _playerInstance.transform;
    }

    private void InitTrack()
    {
        _trackFactory = new(TrackParent, _blockFactory);

        for (int i = 0; i < InitialTrackObjectCount; i++)
            _trackFactory.CreateInstance();
    }
}