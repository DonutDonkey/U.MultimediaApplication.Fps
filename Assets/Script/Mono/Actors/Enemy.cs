using Script.So;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Mono.Actors {
    public class Enemy : UniqueMonoBehaviour, IActor, IKnockbackable, IDamageable {
        [Header("Data Refferences")]
        [SerializeField] private Actor d_actor;
        public Actor DActor => d_actor;

        [Header("Runtime Data")]
        [SerializeField] private int r_health;

        private void Awake()
        {
            r_health = d_actor.health;
        } 

        public Vector3 GetPosition() => this.gameObject.transform.position;
        public Transform GetTransform() => this.gameObject.transform;

        public bool IsDead() => r_health <= 0;

        //TODO: make this work for enemy and also rework on player xd
        public void KnockBack(Vector3 other, float force) {
            // knockback_counter = 0.5f;
            //
            // velocity = (transform.position - other).normalized * force;
            // velocity.y = force > 10f ? force : velocity.y;
        }

        public void TakeDamage(int in_value, Transform in_source) => r_health -= in_value;
    }
}