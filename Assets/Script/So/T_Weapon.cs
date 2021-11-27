using Script.Mono;
using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Templates/Player weapon")]
    public class T_Weapon : ScriptableObject {
        public string id;
        public int damage;
        public float cooldown;
        public int pellets;
        [Range(0.9f, 1)] public float accuracy;

        public int max_distance;

        public AudioClip audio_fire;
        
        [Header("Projectile options")]
        public bool is_projectile;
        public Projectile projectile_prefab;

        [Header("Refferences")]
        public Runtime_IntValue ammo;

        public void UseAmmo() => ammo.runtime_value -= 1;
    }
}