using Script.Mono.Managers;
using UnityEngine;

namespace Script.Mono {
    public class Explosion : MonoBehaviour {
        [Header("Visual data")]
        [SerializeField] private ParticleSystem self_particle;
        
        [Header("Explosion data")]
        [SerializeField] private int max_dmg = 100;
        [SerializeField] private float max_range = 10f;
        [SerializeField, Range(1,5)] private int knockback_scale_factor;
        
        public void Explode() {
            var ep = Manager_Pooler.Instance.Pool_explosion_particle.Get();
            ep.transform.position = transform.position;
            
            Collider[] colliders_hit = new Collider[10];
            
            var size = Physics.OverlapSphereNonAlloc(transform.position, max_range, colliders_hit);
            for (var i = 0; i < size; i++) {
                var damageable = colliders_hit[i].gameObject.GetComponent<IDamageable>();
                var kickbackable = colliders_hit[i].gameObject.GetComponent<IKnockbackable>();
                
                var distance = (int) Vector3.Distance(transform.position, colliders_hit[i].transform.position);
                if (distance < 0) distance = 0;
                
                var out_damage = max_dmg - distance * 10;
                if (out_damage <= 0) continue;
                
                damageable?.TakeDamage(out_damage, transform);
                kickbackable?.KnockBack(transform.position, out_damage/knockback_scale_factor);
            }
            Manager_Pooler.Instance.Pool_explosion.Release(this);
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, max_range);
        }
    }
}