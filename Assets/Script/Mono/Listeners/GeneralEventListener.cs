using Script.So.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Mono.Listeners {
    public class GeneralEventListener : MonoBehaviour, IListener {
        [SerializeField] private GeneralEvent g_event;
        [SerializeField] private UnityEvent response;

        public void OnEventRaised() => response.Invoke();
        private void OnEnable() => g_event?.RegisterListener(this);
        private void OnDisable() => g_event?.UnregisterListener(this);
    }
}