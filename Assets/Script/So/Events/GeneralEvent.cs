using System.Collections.Generic;
using Script.Mono.Listeners;
using UnityEngine;

namespace Script.So.Events {
    [CreateAssetMenu(fileName = "New General Event", menuName = "Events/General Event")]
    public class GeneralEvent : ScriptableObject {
        private readonly List<GeneralEventListener> listeners = new List<GeneralEventListener>();

        public void Raise() {
            for (var i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GeneralEventListener listener) => listeners?.Add(listener);
        public void UnregisterListener(GeneralEventListener listener) => listeners?.Remove(listener);
    }
}