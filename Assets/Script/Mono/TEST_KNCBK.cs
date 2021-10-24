using Script;
using Script.So.Events;
using UnityEngine;

public class TEST_KNCBK : MonoBehaviour {
    [IdAttribute] public string id;
    
    public E_Float @event;
    public float knockback_force;
    
    private void OnTriggerEnter(Collider other) {
        var knockback = other.GetComponent<IKnockbackable>();
        knockback?.KnockBack(transform.position, knockback_force);
    }
}
