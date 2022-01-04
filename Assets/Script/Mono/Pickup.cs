using Script.So.Events;
using UnityEngine;

namespace Script.Mono {
    public class Pickup : MonoBehaviour, IPickup {
        [SerializeField] private E_Int e_pickup_event;
        public int amount;

        public void Invoke() {
            e_pickup_event.Invoke(amount);
            gameObject.SetActive(false);
        }
    }
}