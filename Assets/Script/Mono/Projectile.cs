using System;
using Script.So;
using UnityEngine;

namespace Script.Mono {
    public class Projectile : MonoBehaviour {
        public enum TargetType { None, Player, ENEMY00, ENEMY01, ENEMY02, ENEMY03, ENEMY04, ENEMY05, ENEMY06, ENEMY07,
            ENEMY08, ENEMY09 };
        
        [Header("Weapon data")]
        [SerializeField] private T_Weapon data_weapon;
        
        [Header("Projectile data")]
        [SerializeField] private float projectile_speed;
        [SerializeField] private GameObject projectile_impact_spawn;
        [SerializeField] private TargetType projectile_ignore_tag_on_impact; 

        private Rigidbody rb_projectile;
        
        private Transform pos_projectile_actor_position;
        public Transform PosProjectileActorPosition { get => pos_projectile_actor_position; set => pos_projectile_actor_position = value; }

        private void Update() {
            Debug.Log(projectile_ignore_tag_on_impact.ToString());
        }

        private void Awake() => rb_projectile = GetComponent<Rigidbody>();

        private void OnEnable() => rb_projectile.velocity = transform.forward * projectile_speed;

        private void OnCollisionEnter(Collision other) {
            Debug.Log($"Projectile.OnCollisionEnter() other : {other.gameObject.name}");

            if(other.gameObject.tag.Equals(projectile_ignore_tag_on_impact.ToString())) 
                gameObject.SetActive(false);
            
            var damagable = other.gameObject.GetComponent<IDamageable>();
            
            damagable?.TakeDamage(data_weapon.damage, PosProjectileActorPosition);
            Instantiate(projectile_impact_spawn, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
