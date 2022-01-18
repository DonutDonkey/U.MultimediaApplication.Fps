using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class WeaponPickup : MonoBehaviour, IPickup {
        [SerializeField] private BoolReference BR_weapon;
        [SerializeField] private E_Int E_ammo_event;
        public int amount;

        public GameObject[] deactivate;
        public GameObject activate;
        public void Invoke() {
            if (!BR_weapon.Value) {
                activate.SetActive(true);

                foreach (var go in deactivate)
                    go.SetActive(false);
            }
            BR_weapon.Value = true;
            
            E_ammo_event.Invoke(amount);


            gameObject.SetActive(false);
        }
    }
}