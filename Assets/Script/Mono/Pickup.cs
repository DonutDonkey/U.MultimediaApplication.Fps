using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class Pickup : MonoBehaviour, IPickup {
        [SerializeField] private E_Int e_pickup_event;
        [SerializeField] private Runtime_IntValue ref_val;
        public int amount;

        public void Invoke() {
            if (ref_val.runtime_value + amount > 100) return;
            
            e_pickup_event.Invoke(amount);
            gameObject.SetActive(false);
        }
    }
}