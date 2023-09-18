using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class UITextTemplateFactory
{
    private readonly Transform _playerTransform;
    private readonly Canvas _canvasPrefab;
    private readonly Camera _camera;

    private const int TextAnimationDuration = 2;

    public UITextTemplateFactory(Transform playerTransform, Canvas textPrefab, Camera mainCamera)
    {
        _playerTransform = playerTransform;

        _canvasPrefab = textPrefab;
        _camera = mainCamera;
    }

    public void SpawnTextTemplate()
    {
        Canvas newCanvas = Object.Instantiate(_canvasPrefab, _playerTransform.position, quaternion.identity);
        newCanvas.worldCamera = _camera;

        float resultYPosition = newCanvas.transform.position.y - 50;
        newCanvas.transform.DOMoveY(resultYPosition, TextAnimationDuration).SetEase(Ease.InSine).OnComplete(() => Object.Destroy(newCanvas.gameObject));
    }
}