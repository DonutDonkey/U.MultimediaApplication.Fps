using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "new Weapon", menuName = "Templates/Player weapon")]
    public class T_Weapon : ScriptableObject {
        public string id;
        public int damage;
        public float cooldown;
        public float accuracy;
        
        [Header("Projectile options")]
        public bool is_projectile;
        public GameObject projectile_prefab;
    }
}