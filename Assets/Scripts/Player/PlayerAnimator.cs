using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _playerRagdollAnimator;
    [SerializeField] private Animator _playerModelAnimator;
    
    private Rigidbody[] _ragdollBodies;
    
    private static readonly int Jump = Animator.StringToHash("Jump");

    public void Awake()
    {
        _ragdollBodies = _playerRagdollAnimator.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody body in _ragdollBodies)
            body.isKinematic = true;
    }

    public void TriggerJumpAnimation()
    {
        _playerModelAnimator.SetTrigger(Jump);
    }
    
    public void TriggerRagdollPhysics()
    {
        _playerModelAnimator.enabled = false;
        _playerModelAnimator.gameObject.SetActive(false);
        
        _playerRagdollAnimator.enabled = false;
        _playerRagdollAnimator.gameObject.SetActive(true);
        
        foreach (Rigidbody body in _ragdollBodies)
            body.isKinematic = false;
    }
}