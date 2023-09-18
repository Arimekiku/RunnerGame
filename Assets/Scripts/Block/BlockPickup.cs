using System;
using UnityEngine;

public class BlockPickup : MonoBehaviour
{
    public event Action OnPickupEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour _))
        {
            OnPickupEvent?.Invoke();
            Destroy(gameObject);
        } 
    }
}
