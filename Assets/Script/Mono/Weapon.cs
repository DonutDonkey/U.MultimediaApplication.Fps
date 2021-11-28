using System;
using System.Collections.Generic;
using Script.Mono.Actors.Player;
using Script.Mono.Listeners;
using Script.Mono.Managers;
using Script.So;
using Script.So.Events;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Mono {
    public class Weapon : MonoBehaviour, IWeapon {
        [Header("Weapon data")]
        [SerializeField] private T_Weapon weapon_data;
        [SerializeField] private ParticleSystem hitscan_particle_impact_environment;
        [SerializeField] private ParticleSystem hitscan_particle_impact_enemy;

        [Header("Event management")]
        [SerializeField] private GeneralEvent e_weapon_blowback;

        [SerializeField] private E_AudioClip e_weapon_audio;
        [SerializeField] private E_String e_weapon_switch;
        [SerializeField] private CodeListener<string, E_String> l_weapon_switch;
        
        private float cooldown = 0.0f;
        private Action attack;

        private bool switching = false;
        
        private Transform projectile_init_transform;
        private Transform ProjectileInitTransform => projectile_init_transform ??= GameObject.Find("WEP04 : PROJECTILE : TRANSFORM").transform;

        [Header("Pooling")]
        public PoolType pool_type;
        public enum PoolType { Stack, LinkedList }
       
        private IObjectPool<Projectile> pool_projectile;
        public IObjectPool<Projectile> PoolProjectile { 
            get { return pool_projectile ??= pool_type switch {
                    PoolType.Stack => new ObjectPool<Projectile>(
                        Pool_Projectile_CreateFunc,
                        Pool_Projectile_OnGet, 
                        Pool_Projectile_OnRelease, 
                        Pool_Projectile_OnDestroy, 
                        false, 
                        5, 
                        10),
                    PoolType.LinkedList => new LinkedPool<Projectile>(
                        Pool_Projectile_CreateFunc, 
                        Pool_Projectile_OnGet,
                        Pool_Projectile_OnRelease, 
                        Pool_Projectile_OnDestroy, 
                        false, 
                        10),
                    _ => throw new ArgumentOutOfRangeException()
                }; }
        }
        
        #region PROJECTILE POOL SETTINGS

        private Transform _parent;
        private Transform Parent => _parent ??= GameObject.Find("POOL").transform;
        
        private Projectile Pool_Projectile_CreateFunc() {
            var p = Instantiate(weapon_data.projectile_prefab, ProjectileInitTransform.position, ProjectileInitTransform.rotation, Parent);
            p.transform.forward = ProjectileInitTransform.forward;
            
            p.PosProjectileActorPosition = transform;
            p.Pool = PoolProjectile;
            
            return p;
        }
        
        private void Pool_Projectile_OnGet(Projectile in_projectile) {
            in_projectile.transform.position = ProjectileInitTransform.position;
            in_projectile.transform.rotation = ProjectileInitTransform.rotation;
            in_projectile.transform.forward = ProjectileInitTransform.forward;
            
            in_projectile.gameObject.SetActive(true);
        }

        private void Pool_Projectile_OnRelease(Projectile in_projectile) => in_projectile.gameObject.SetActive(false);
        private void Pool_Projectile_OnDestroy(Projectile in_projectile) => Destroy(in_projectile.gameObject);
        
        #endregion
        
        private void Awake() {
            if (weapon_data.is_projectile)
                attack = Projectile;
            else
                attack = Hitscan;
            switching = false;
        }

        private void Switch(string in_w_name) {
            if (in_w_name == weapon_data.id) return;
            if (switching) return;
            
            GetComponent<Animator>().Play("Hide");
            switching = true;
        }

        private void OnEnable() {
            GetComponent<Animator>().Play("Selected");
            l_weapon_switch.OnEnable(Switch);
            switching = false;
        }

        private void OnDisable() => l_weapon_switch.OnDisable();
        private void Update() {
            cooldown += Time.deltaTime;
            
            //Check ammo count
            if(Input.GetButton(World_Constants.INPUT_ATTACK) && cooldown > weapon_data.cooldown)
                attack.Invoke();
            
            
            if(Input.GetKeyDown(KeyCode.Alpha1))
                e_weapon_switch.Invoke(World_Constants.ID_WEAPON_1);            
            if(Input.GetKeyDown(KeyCode.Alpha2))
                e_weapon_switch.Invoke(World_Constants.ID_WEAPON_2);
            if(Input.GetKeyDown(KeyCode.Alpha3))
                e_weapon_switch.Invoke(World_Constants.ID_WEAPON_3);
            if(Input.GetKeyDown(KeyCode.Alpha4))
                e_weapon_switch.Invoke(World_Constants.ID_WEAPON_4);
        }
#if UNITY_EDITOR
        public List<Vector3> debug_transformy;
#endif
        public void Hitscan() {
            Debug.Log($"{weapon_data.id} HITSCAN");
           
            GetComponent<Animator>().Play("Attack");
            
            // TODO: generate based on cam.main transform
            if (weapon_data.audio_fire != null) 
                e_weapon_audio.Invoke(weapon_data.audio_fire);
            e_weapon_blowback.Invoke();
            cooldown = 0.0f;

            for (var i = 0; i < weapon_data.pellets; i++) {
                var x_spread =
                    UnityEngine.Random.Range(-1 * (1 - weapon_data.accuracy), 1 * (1 - weapon_data.accuracy));
                var y_spread =
                    UnityEngine.Random.Range(-1 * (1 - weapon_data.accuracy), 1 * (1 - weapon_data.accuracy));

                var target = Camera.main.transform.forward;
                target.x += x_spread;
                target.y += y_spread;

                // Some bit shifting shit https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                if (!Physics.Raycast(Camera.main.transform.position, target, out RaycastHit hit,
                    weapon_data.max_distance, layerMask)) continue;

                var damageable = hit.transform.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(weapon_data.damage, GetComponentInParent<Player>().transform);

                if (hit.transform.gameObject.tag.Contains("ENEMY")) {
                    var hs_e = Manager_Pooler.Instance.Pool_hitscan_blood.Get();
                    hs_e.transform.position = hit.point;
                    hs_e.transform.rotation = Quaternion.LookRotation(hit.normal);
                } else {
                    var hs_b = Manager_Pooler.Instance.Pool_hitscan_env.Get();
                    hs_b.transform.position = hit.point;
                    hs_b.transform.rotation = Quaternion.LookRotation(hit.normal);
                    
                    var decal = Manager_Pooler.Instance.Pool_hitscan_decal.Get();
                    decal.transform.position = hit.point;
                    decal.transform.rotation = Quaternion.LookRotation(hit.normal);
                }

                Debug.Log($"HIT : {hit.transform.gameObject.name}");
#if UNITY_EDITOR
                debug_transformy.Add(hit.point);
#endif
                weapon_data.UseAmmo();
            }
        }
        
        public void Projectile() {
            Debug.Log($"{weapon_data.id} : Weapon.Projectile()");
            GetComponent<Animator>().Play("Attack");
            
            if (weapon_data.audio_fire != null) 
                e_weapon_audio.Invoke(weapon_data.audio_fire);
            e_weapon_blowback.Invoke();
            
            cooldown = 0.0f;
            
            PoolProjectile.Get();
            weapon_data.UseAmmo();
        }

        public void AnimationFinished() => gameObject.SetActive(false);

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;

            foreach (var dt in debug_transformy) {
                Gizmos.DrawSphere(dt, 0.2f);
            }
        }
#endif
    }
}