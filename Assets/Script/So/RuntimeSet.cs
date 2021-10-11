using System.Collections.Generic;
using UnityEngine;

namespace Script.So {
    public class RuntimeSet<T> : ScriptableObject, IRuntimeSet<T>{
        private List<T> items = new List<T>();

        public void Initialize() => items.Clear();

        public void AddToList(T item) {
            if(!items.Contains(item))
                items.Add(item);
        }         
        
        public void RemoveFromList(T item) {
            if(items.Contains(item))
                items.Remove(item);
        }
        
        public T GetItemIndex(int index) => items[index];
    }
}