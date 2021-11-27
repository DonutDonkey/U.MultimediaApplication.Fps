using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Mono {
    public abstract class Pool <T> : MonoBehaviour where T : Component {
        [Header("Pooling")]
        public PoolType pool_type;
        public enum PoolType { Stack, LinkedList }
        
        private T _self;

        private IObjectPool<T> pool_projectile;
        public IObjectPool<T> PoolProjectile { 
            get {
                return pool_projectile ??= pool_type switch {
                    PoolType.Stack => new ObjectPool<T>(
                        () => Instantiate(_self), 
                        Pool_OnGet, 
                        Pool_OnRelease, 
                        Pool_OnDestroy, 
                        false, 
                        10, 
                        20),
                    PoolType.LinkedList => new LinkedPool<T>(
                        () => Instantiate(_self), 
                        Pool_OnGet, 
                        Pool_OnRelease, 
                        Pool_OnDestroy, 
                        false, 
                        20),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        } 
        
        private static void Pool_OnGet<T>(T in_item) where T : Component => in_item.gameObject.SetActive(true);
        private static void Pool_OnRelease<T>(T in_item) where T : Component => in_item.gameObject.SetActive(false);
        private static void Pool_OnDestroy<T>(T in_item) where T : Component => Destroy(in_item.gameObject);
    }
}