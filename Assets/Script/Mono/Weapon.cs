using System;
using Script.Mono.Listeners;
using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class Weapon : MonoBehaviour, IWeapon {
        [Header("Weapon data")]
        [SerializeField] private T_Weapon weapon_data;
        
        [Header("Event management")]
        [SerializeField] private E_String e_swtich_weapon;
        [SerializeField] private CodeListener<string, E_String> l_switch_weapon;

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
            switching = false;
            GetComponent<Animator>().Play("Selected");
            l_switch_weapon.OnEnable(Switch);
        }

        private void OnDisable() => l_switch_weapon.OnDisable();

        private void Update() {
            cooldown += Time.deltaTime;
            
            if(Input.GetButton(World_Constants.INPUT_ATTACK) && cooldown > weapon_data.cooldown)
                attack.Invoke();
            if(Input.GetKeyDown(KeyCode.Alpha1))
                e_swtich_weapon.Invoke(World_Constants.ID_WEAPON_1);            
            if(Input.GetKeyDown(KeyCode.Alpha2))
                e_swtich_weapon.Invoke(World_Constants.ID_WEAPON_2);
            if(Input.GetKeyDown(KeyCode.Alpha3))
                e_swtich_weapon.Invoke(World_Constants.ID_WEAPON_3);
            if(Input.GetKeyDown(KeyCode.Alpha4))
                e_swtich_weapon.Invoke(World_Constants.ID_WEAPON_4);
        }

        public void Hitscan() {
            Debug.Log($"{weapon_data.id} HITSCAN");
            cooldown = 0.0f;
        }

        public void Projectile() {
            Debug.Log($"{weapon_data.id} Projectile");
            cooldown = 0.0f;
        }

        public void AnimationFinished() => gameObject.SetActive(false);
    }
}