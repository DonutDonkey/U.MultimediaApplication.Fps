using System;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono.Listeners {
    [System.Serializable]
    public class CodeListener<T, E> : IListener<T> where E : Event<T> {
        [SerializeField] private E @event;
        private Action<T> response;

        public void OnEventRaised(T item) => response?.Invoke(item);

        public void OnEnable(Action<T> item) {
            @event?.RegisterListener(this);
            response = item;
        }

        public void OnDisable() {
            @event?.UnregisterListener(this);
            response = null;
        }
    }
}