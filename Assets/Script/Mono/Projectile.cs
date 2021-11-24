using Script.So;
using UnityEngine;

namespace Script.Mono {
    public class Projectile : MonoBehaviour {
        [Header("Weapon data")]
        [SerializeField] private T_Weapon data_weapon;
        
        [Header("Projectile data")]
        [SerializeField] private float projectile_speed;
        [SerializeField] private GameObject projectile_impact_spawn;

        private Rigidbody rb_projectile;
        
        private Transform pos_projectile_actor_position;
        public Transform PosProjectileActorPosition { get => pos_projectile_actor_position; set => pos_projectile_actor_position = value; }

        private void Awake() => rb_projectile = GetComponent<Rigidbody>();

        private void OnEnable() => rb_projectile.velocity = transform.forward * projectile_speed;

        private void OnCollisionEnter(Collision other) {
            Debug.Log($"Projectile.OnCollisionEnter() other : {other.gameObject.name}");

            var damagable = other.gameObject.GetComponent<IDamageable>();
            
            damagable?.TakeDamage(data_weapon.damage, PosProjectileActorPosition);
            Instantiate(projectile_impact_spawn, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
