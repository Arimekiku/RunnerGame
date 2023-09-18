using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _restartSection;
    [SerializeField] private CanvasGroup _startSection;

    public event Action OnRestartShow;
    
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowRestartSection()
    {
        OnRestartShow?.Invoke();
        _restartSection.DOFade(1, 1f);
    }

    public void ToggleStartSection(bool isVisible)
    {
        _startSection.DOFade(isVisible ? 1 : 0, 1f);
    }
}