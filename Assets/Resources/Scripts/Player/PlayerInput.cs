using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerBehaviour _playerBehaviour;
    private UIManager _uiManager;

    private bool _gameDisabled;

    public void Init(UIManager uiManager)
    {
        _playerBehaviour = GetComponent<PlayerBehaviour>();
        _uiManager = uiManager;
    }

    private void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        if (_gameDisabled) 
            return;
        
        if (Input.GetMouseButtonDown(0))
            _uiManager.ToggleStartSection(false);

        if (Input.GetMouseButton(0))
        {
            //Should get number between -2 and 2 in order to move on platform
            //So we divide current mouse position with screen resolution (0; 1)
            //Then multiply by 4 (0; 4) and minus 2 so we get [-2; 2]
            float castedWorldPosition = Input.mousePosition.x / Screen.currentResolution.width * 4 - 2;

            _playerBehaviour.MoveSideways(castedWorldPosition);
            _playerBehaviour.MoveForward();
        }

        if (Input.GetMouseButtonUp(0))
            _uiManager.ToggleStartSection(true);
    }

    public void DisableInput()
    {
        _gameDisabled = true;
    }
}