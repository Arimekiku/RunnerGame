using System;
using UnityEngine;
using VContainer.Unity;

public class PlayerInput : IPlayerInput, ITickable
{
    public event Action<float> OnScreenHold;
    public event Action<bool> OnScreenTap;

    private bool _gameDisabled;

    public void Tick()
    {
        if (_gameDisabled) 
            return;
        
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        if (Input.GetMouseButtonDown(0))
            OnScreenTap?.Invoke(true);

        if (Input.GetMouseButton(0))
        {
            //Should get number between -2 and 2 in order to move on platform
            //So we divide current mouse position with screen resolution (0; 1)
            //Then multiply by 4 (0; 4) and minus 2 so we get [-2; 2]
            float castedWorldPosition = Input.mousePosition.x / Screen.currentResolution.width * 4 - 2;
            
            OnScreenHold?.Invoke(castedWorldPosition);
        }

        if (Input.GetMouseButtonUp(0))
            OnScreenTap?.Invoke(false);
    }

    public void DisableInput()
    {
        _gameDisabled = true;
    }
}