using System;
using Script.Mono.Managers;
using Script.So;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Mono {
    public class Projectile : MonoBehaviour {
        public enum TargetType { None, Player, ENEMY00, ENEMY01, ENEMY02, ENEMY03, ENEMY04, ENEMY05, ENEMY06, ENEMY07,
            ENEMY08, ENEMY09 };

        public enum TagAction { None, Deactivate, Continue };
        
        [Header("Weapon data")]
        [SerializeField] private T_Weapon data_weapon;
        [SerializeField] private int damage_const;
        
        [Header("Projectile data")]
        [SerializeField] private float projectile_speed;
        [SerializeField] private TargetType projectile_tag_aware;
        [SerializeField] private TagAction projectile_tag_action;
        //TODO: add enum for what to spawn if anything

        private Rigidbody rb_projectile;
        
        private Transform pos_projectile_actor_position;
        public Transform PosProjectileActorPosition { get => pos_projectile_actor_position; set => pos_projectile_actor_position = value; }


        public IObjectPool<Projectile> pool;

        private int damage;
        private void Update() {
            Debug.Log(projectile_tag_aware.ToString());
        }

        private void Awake() {
            rb_projectile = GetComponent<Rigidbody>();
            damage = data_weapon != null ? data_weapon.damage : damage_const;
        }

        private void OnEnable() => rb_projectile.velocity = transform.forward * projectile_speed;

        private void OnCollisionEnter(Collision other) {
            Debug.Log($"Projectile.OnCollisionEnter() other : {other.gameObject.name}");

            if(other.gameObject.tag.Equals(projectile_tag_aware.ToString()))
                switch (projectile_tag_action) {
                    case TagAction.None:
                        break;
                    case TagAction.Deactivate:
                        pool.Release(this);
                        break;
                    case TagAction.Continue:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            
            var damagable = other.gameObject.GetComponent<IDamageable>();
            damagable?.TakeDamage(damage, PosProjectileActorPosition);

            if (!gameObject.tag.Contains("ENEMY")) {
                var expl = Manager_Pooler.Instance.Pool_explosion.Get();
                expl.transform.position = gameObject.transform.position;
                expl.Explode();
            }
            pool.Release(this);
        }
    }
}
