using UnityEngine;
using UnityEngine.Events;

namespace Script.Mono {
    public class InteractWithTag : MonoBehaviour {
        public string other_tag;
        public UnityEvent @event;
        
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag.Equals(other_tag))
                @event.Invoke();
        }
    }
}