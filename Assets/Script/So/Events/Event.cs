using System.Collections.Generic;
using Script.Mono.Listeners;
using UnityEngine;

namespace Script.So.Events {
    public abstract class Event<T> : ScriptableObject {
        private readonly List<IListener<T>> listeners = new List<IListener<T>>();

        public void Raise(T item) {
            for (var i = listeners.Count - 1; i >= 0; i--) 
                listeners[i]?.OnEventRaised(item);
        }

        public void RegisterListener(IListener<T> listener) => listeners?.Add(listener);
        public void UnregisterListener(IListener<T> listener) => listeners?.Remove(listener);
    }
}