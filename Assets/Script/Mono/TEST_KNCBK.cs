using Script;
using UnityEngine;

public class TEST_KNCBK : MonoBehaviour {
    [IdAttribute] public string id;
    public float knockback_force;

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        var knockback = other.GetComponent<IKnockbackable>();
        knockback?.KnockBack(transform.position, knockback_force);
        
        var dmg = other.GetComponent<IDamageable>();
        dmg?.TakeDamage((int)knockback_force, transform);
    }
}
