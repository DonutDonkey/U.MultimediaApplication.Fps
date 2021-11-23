using Script.So;
using UnityEngine;

namespace Script.Mono {
    public class Projectile : MonoBehaviour {
        [Header("Weapon data")]
        [SerializeField] private T_Weapon data_weapon;
        
        [Header("Projectile data")]
        [SerializeField] private float projectile_speed;
        [SerializeField] private float projectile_radius;

        private Rigidbody rb_projectile;
        
        private Transform pos_projectile_actor_position;
        public Transform PosProjectileActorPosition { get => pos_projectile_actor_position; set => pos_projectile_actor_position = value; }

        private void Awake() => rb_projectile = GetComponent<Rigidbody>();

        // Might be able to delete this entire function in real time since levels won't be designed with infinite space..
        private Vector3 pos_starting;
        private void Update() {
            if (Vector3.Distance(pos_starting, transform.position) > data_weapon.max_distance)
                gameObject.SetActive(false);
        }

        private void OnEnable() {
            pos_starting = transform.position;
            // rb_projectile.velocity = transform.forward * projectile_speed;
        }

        private void OnCollisionEnter(Collision other) {
            Collider[] colliders_hit = new Collider[10];
            var size = Physics.OverlapSphereNonAlloc(transform.position, projectile_radius, colliders_hit);

            foreach (var c in colliders_hit) {
                var damage = c.gameObject.GetComponent<IDamageable>();
                var knockback = c.gameObject.GetComponent<IKnockbackable>();

                var distance = Vector3.Distance(transform.position, c.gameObject.transform.position);
                damage?.TakeDamage(data_weapon.damage, pos_projectile_actor_position);
                knockback?.KnockBack(transform.position, 100);
            }
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, projectile_radius);
        }
    }
}
