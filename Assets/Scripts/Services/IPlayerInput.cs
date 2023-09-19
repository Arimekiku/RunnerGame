using System;

public interface IPlayerInput
{
    public event Action<float> OnScreenHold;
    public event Action<bool> OnScreenTap;

    public void DisableInput();
}