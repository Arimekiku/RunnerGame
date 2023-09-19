using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using VContainer;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup RestartSection;
    [SerializeField] private CanvasGroup StartSection;

    private IPlayerInput _playerInput;
    
    [Inject]
    public void Init(BlockManager blockManager, IPlayerInput playerInput)
    {
        blockManager.OnLose += ShowRestartSection;
        playerInput.OnScreenTap += ToggleStartSection;
        
        _playerInput = playerInput;
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowRestartSection()
    {
        _playerInput.DisableInput();
        RestartSection.DOFade(1, 1f);
    }

    private void ToggleStartSection(bool isVisible)
    {
        StartSection.DOFade(isVisible ? 1 : 0, 1f);
    }
}