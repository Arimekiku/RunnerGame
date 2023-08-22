using Cinemachine;
using UnityEngine;

public class CameraScreenShakeTrigger : MonoBehaviour
{
    private CinemachineImpulseSource _impulse;
    
    private const float ImpulseForce = 1f;

    public void Init()
    {
        _impulse = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBlock _)) 
            _impulse.GenerateImpulseWithForce(ImpulseForce);
    }
}