using System;
using Script.So.Events;
using UnityEngine;

public class TEST_KNCBK : MonoBehaviour {
    public E_Float @event;
    public float knockback_force;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Player"))
            @event.Invoke(knockback_force);
    }
}
