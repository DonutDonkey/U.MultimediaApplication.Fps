using UnityEngine;

namespace Script {
    public interface IDamageable {
        public void TakeDamage(int in_value, Transform in_source);
    }
}