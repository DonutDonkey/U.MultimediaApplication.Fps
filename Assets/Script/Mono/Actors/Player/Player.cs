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
            // IF damage above certain treshold knockback from controller
            e_player_remove_hp.Invoke(in_value);
            e_player_dmgSource.Invoke(in_source);
        }
    }
}