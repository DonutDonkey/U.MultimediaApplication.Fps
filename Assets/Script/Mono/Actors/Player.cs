using Script.So;
using UnityEngine;

namespace Script.Mono.Actors {
    public class Player : MonoBehaviour, IActor, IDamageable {
        [Header("Player runtime data")]
        [SerializeField] private Runtime_IntValue r_health;
        [SerializeField] private Runtime_IntValue r_armor;
        
        public Vector3 GetPosition() => this.transform.position;

        public Transform GetTransform() => this.gameObject.transform;

        public bool IsDead() => r_health > 0;

        public void TakeDamage(int in_value) {
            throw new System.NotImplementedException();
            
            // IF damage above certain treshold knockback from controller
        }
    }
}