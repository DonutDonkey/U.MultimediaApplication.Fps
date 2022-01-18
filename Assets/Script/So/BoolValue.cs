using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Bool Value", menuName = "Values/Bool")]
    public class BoolValue : ScriptableObject {
        public bool value;

        public static implicit operator bool(BoolValue boolValue) => boolValue.value;
    }
}