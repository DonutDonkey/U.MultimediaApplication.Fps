using Script.So;
using UnityEngine;

namespace Script.Mono {
    public class AddToRuntimeSet<T> : MonoBehaviour {
        public RuntimeSet_GameObject runtime_set_go;

        private void OnEnable() => runtime_set_go.AddToList(this.gameObject);

        private void OnDisable() => runtime_set_go.RemoveFromList(this.gameObject);
    }
}