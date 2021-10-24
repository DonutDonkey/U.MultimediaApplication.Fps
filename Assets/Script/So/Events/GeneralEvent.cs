using System.Collections.Generic;
using Script.Mono.Listeners;
using UnityEngine;

namespace Script.So.Events {
    [CreateAssetMenu(fileName = "New General Event", menuName = "Events/General Event")]
    public class GeneralEvent : ScriptableObject {
        private readonly HashSet<IListener> listeners = new HashSet<IListener>();

        public void Invoke() {
            foreach(var l in listeners)
                l?.OnEventRaised();
        }

        public void RegisterListener(IListener listener) => listeners?.Add(listener);
        public void UnregisterListener(IListener listener) => listeners?.Remove(listener);
    }
}