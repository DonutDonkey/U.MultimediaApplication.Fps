using System.Collections.Generic;
using Script.Mono.Listeners;
using UnityEngine;

namespace Script.So.Events {
    public abstract class Event<T> : ScriptableObject {
        private readonly HashSet<IListener<T>> listeners = new HashSet<IListener<T>>();

        public void Invoke(T item) {
            foreach (var l in listeners)
                l?.OnEventRaised(item);
        }

        public void RegisterListener(IListener<T> listener) => listeners?.Add(listener);
        public void UnregisterListener(IListener<T> listener) => listeners?.Remove(listener);
    }
}