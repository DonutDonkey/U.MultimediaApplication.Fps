using System;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono.Listeners {
    public abstract class CodeListener<T, E> : IListener<T> where E : Event<T> {
        [SerializeField] private E @event;
        private Action<T> response;

        public void OnEventRaised(T item) => response?.Invoke(item);

        private void OnEnable(Action<T> item) {
            @event?.RegisterListener(this);
            response = item;
        }

        private void OnDisable() {
            @event?.UnregisterListener(this);
            response = null;
        }
    }
}