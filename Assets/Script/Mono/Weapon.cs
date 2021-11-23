using System;
using System.Collections.Generic;
using Script.Mono.Listeners;
using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class Weapon : MonoBehaviour, IWeapon {
        [Header("Weapon data")]
        [SerializeField] private T_Weapon weapon_data;
        
        [Header("Event management")]
        [SerializeField] private GeneralEvent e_weapon_blowback;
        
        [SerializeField] private E_String e_weapon_switch;
        [SerializeField] private CodeListener<string, E_String> l_weapon_switch;

        private float cooldown = 0.0f;
        private Action attack;

        private bool switching = false;
        
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
            e_weapon_blowback.Invoke();
            cooldown = 0.0f;
            
            for (var i = 0; i < weapon_data.pellets; i++) {
                var x_spread = UnityEngine.Random.Range(-1*(1 - weapon_data.accuracy), 1*(1 - weapon_data.accuracy));
                var y_spread = UnityEngine.Random.Range(-1*(1 - weapon_data.accuracy), 1*(1 - weapon_data.accuracy));
                
                var target = Camera.main.transform.forward;
                target.x += x_spread;
                target.y += y_spread;

                if (!Physics.Raycast(Camera.main.transform.position, target, out RaycastHit hit,
                    weapon_data.max_distance)) continue;
                
                Debug.Log($"HIT : {hit.transform.gameObject.name}");
#if UNITY_EDITOR
                debug_transformy.Add(hit.point);
#endif
            }
        }
        
        public void Projectile() {
            Debug.Log($"{weapon_data.id} Projectile");
            cooldown = 0.0f;
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