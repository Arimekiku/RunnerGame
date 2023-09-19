using DG.Tweening;
using UnityEngine;

public class DisposableWorldText : MonoBehaviour
{
    private const float TextAnimationDuration = 2f;
    
    private void Awake()
    {
        float resultYPosition = transform.position.y - 50;
        transform.DOMoveY(resultYPosition, TextAnimationDuration).SetEase(Ease.InSine).OnComplete(() => Destroy(gameObject));
    }
}