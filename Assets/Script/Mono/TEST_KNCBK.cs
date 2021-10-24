using Script;
using Script.So.Events;
using UnityEngine;

public class TEST_KNCBK : MonoBehaviour {
    [IdAttribute] public string id;
    public float knockback_force;

    public GeneralEvent shit;
    public E_Int dmg;
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        var knockback = other.GetComponent<IKnockbackable>();
        knockback?.KnockBack(transform.position, knockback_force);
        
        shit.Invoke();
        dmg.Invoke((int)knockback_force);
    }
}
