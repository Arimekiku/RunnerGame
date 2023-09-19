using System;
using UnityEngine;
using VContainer;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody PlayerModelBody;
    [SerializeField] private PlayerAnimator PlayerAnimator;
    [SerializeField] private Transform PlayerCubeHolder;
    [SerializeField] private Vector3 Velocity;
    
    public Transform CubeHolder => PlayerCubeHolder.transform;

    public event Action OnNewBlock;
    
    [Inject]
    public void Init(IPlayerInput playerInput, BlockManager blockManager)
    {
        playerInput.OnScreenHold += position =>
        {
            MoveSideways(position);
            MoveForward();
        };

        blockManager.OnLose += PlayerAnimator.TriggerRagdollPhysics;
    }
    
    public void Awake()
    {
        PlayerModelBody.isKinematic = false;
    }

    private void MoveForward()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }

    private void MoveSideways(float xOffset)
    {
        Vector3 newPosition = new(xOffset, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    
    public void OnBlockSpawned(Vector3 newPosition)
    {
        PlayerAnimator.TriggerJumpAnimation();
        PlayerModelBody.transform.position = newPosition;
        
        OnNewBlock?.Invoke();
    }
}