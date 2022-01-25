using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class Pickup : MonoBehaviour, IPickup {
        [SerializeField] private E_Int e_pickup_event;
        [SerializeField] private Runtime_IntValue ref_val;
        public int amount;

        public void Invoke() {
            if (ref_val.runtime_value > 100) return;
            
            if (ref_val.runtime_value + amount > 100) 
                amount = 100 - ref_val.runtime_value;
            
            e_pickup_event.Invoke(amount);
            gameObject.SetActive(false);
        }
    }
}