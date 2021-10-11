using Script.So.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Mono.Listeners {
    public abstract class Listener<T, E, UER> : MonoBehaviour, IListener<T> where E : Event<T> where UER: UnityEvent<T> {
        [SerializeField] private E game_event;
        [SerializeField] private UER unity_event_response;
        
        public void OnEventRaised(T item) => unity_event_response?.Invoke(item);
        private void OnEnable() => game_event?.RegisterListener(this);
        private void OnDisable() => game_event?.UnregisterListener(this);
    }
}