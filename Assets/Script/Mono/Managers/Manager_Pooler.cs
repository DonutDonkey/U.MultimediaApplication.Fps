using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Mono.Managers {
    public class Manager_Pooler : Singleton<Manager_Pooler> {
        public enum PoolType {
            Stack,
            LinkedList
        }

        [SerializeField] private Explosion prefab_explosion;
        [SerializeField] private PoolType prefab_explosion_pt;
        [SerializeField] private ParticleSystem prefab_explosion_particle;
        [SerializeField] private PoolType prefab_explosion_particle_pt;
        
        [SerializeField] private ParticleSystem weapon_hitscan_environment;
        [SerializeField] private PoolType weapon_hitscan_environment_pt;
        [SerializeField] private ParticleSystem weapon_hitscan_blood;
        [SerializeField] private PoolType weapon_hitscan_blood_pt;

        private GameObject parent;
        private GameObject Parent => parent ??= GameObject.Find("POOL"); 
        
        private IObjectPool<Explosion> pool_explosion;
        public IObjectPool<Explosion> Pool_explosion { get { return pool_explosion ??= prefab_explosion_pt switch {
                    PoolType.Stack => new ObjectPool<Explosion>(
                        () => Instantiate(prefab_explosion, Parent.transform),
                        in_item => in_item.gameObject.SetActive(true),
                        in_item => in_item.gameObject.SetActive(false),
                        in_item => Destroy(in_item.gameObject)
                    ),
                    PoolType.LinkedList => new LinkedPool<Explosion>(
                        () => Instantiate(prefab_explosion),
                        in_item => in_item.gameObject.SetActive(true),
                        in_item => in_item.gameObject.SetActive(false),
                        in_item => Destroy(in_item.gameObject)
                    ),
                    _ => throw new ArgumentOutOfRangeException()
                }; }
        }

        private IObjectPool<ParticleSystem> pool_explosion_particle;
        public IObjectPool<ParticleSystem> Pool_explosion_particle { get { return pool_explosion_particle ??= prefab_explosion_particle_pt switch {
                PoolType.Stack => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(prefab_explosion_particle, Parent.gameObject.transform);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_explosion_particle;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),                
                PoolType.LinkedList => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(prefab_explosion_particle);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_explosion_particle;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),
                _ => throw new ArgumentOutOfRangeException()
            }; } 
        }        
        
        private IObjectPool<ParticleSystem> pool_hitscan_env;
        public IObjectPool<ParticleSystem> Pool_hitscan_env { get { return pool_hitscan_env ??= weapon_hitscan_environment_pt switch {
                PoolType.Stack => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(weapon_hitscan_environment, Parent.transform);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_hitscan_env;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),                
                PoolType.LinkedList => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(weapon_hitscan_environment);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_hitscan_env;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),
                _ => throw new ArgumentOutOfRangeException()
            }; } 
        }        
        
        private IObjectPool<ParticleSystem> pool_hitscan_blood;
        public IObjectPool<ParticleSystem> Pool_hitscan_blood { get { return pool_hitscan_blood ??= weapon_hitscan_blood_pt switch {
                PoolType.Stack => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(weapon_hitscan_blood, Parent.transform);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_hitscan_blood;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),                
                PoolType.LinkedList => new ObjectPool<ParticleSystem>(
                    () => { var ps = Instantiate(weapon_hitscan_blood);
                                    var rp = ps.AddComponent<ReturnToPool>();
                                    rp._pool = pool_hitscan_blood;
                                    return ps; },
                    in_item => in_item.gameObject.SetActive(true),
                    in_item => in_item.gameObject.SetActive(false),
                    in_item => Destroy(in_item.gameObject)
                ),
                _ => throw new ArgumentOutOfRangeException()
            }; } 
        }
    }

    [RequireComponent(typeof(ParticleSystem))]
    public class ReturnToPool : MonoBehaviour {
        public IObjectPool<ParticleSystem> _pool;
        private ParticleSystem system;

        private void Start() {
            system = GetComponent<ParticleSystem>();
            var main = system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
        
        private void OnParticleSystemStopped() => _pool.Release(system);
    }
}