using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono.Actors.Player {
    public class Player : MonoBehaviour, IActor, IDamageable {
        [Header("Player runtime data")]
        [SerializeField] private Runtime_IntValue r_health;
        [SerializeField] private Runtime_IntValue r_armor;

        [Header("Events")]
        [SerializeField] private E_Int e_player_remove_hp;
        [SerializeField] private E_Int e_player_remove_armor;
        [SerializeField] private E_Transform e_player_dmgSource;
        public Vector3 GetPosition() => this.transform.position;

        public Transform GetTransform() => this.gameObject.transform;

        public bool IsDead() => r_health > 0;

        public void TakeDamage(int in_value, Transform in_source) {
            var dmg_armor = in_value / 2;
            var dmg_hp = r_armor.runtime_value - dmg_armor >= 0 ? in_value / 2 : in_value - r_armor;
            // IF damage above certain treshold knockback from controller
            e_player_remove_hp.Invoke(dmg_hp);
            
            e_player_remove_armor.Invoke(dmg_armor);
            NormalizeRuntimeArmor();
            
            e_player_dmgSource.Invoke(in_source);
        }

        private void NormalizeRuntimeArmor() =>
            r_armor.runtime_value = r_armor.runtime_value < 0 ? 0 : r_armor.runtime_value;
    }
}