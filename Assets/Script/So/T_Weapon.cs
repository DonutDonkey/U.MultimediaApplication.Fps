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
        
        [Header("Projectile options")]
        public bool is_projectile;
        public GameObject projectile_prefab;
    }
}