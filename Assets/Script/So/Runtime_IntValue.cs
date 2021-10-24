using System;
using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Runtime Int Value", menuName = "Runtime Values/Int")]
    public class Runtime_IntValue : ScriptableObject, ISerializationCallbackReceiver {
        public int initial_value;
        
        [NonSerialized] public int runtime_value;

        public static implicit operator int(Runtime_IntValue intValue) => intValue.runtime_value;

        public static Runtime_IntValue operator -(Runtime_IntValue intValue, int value) {
            intValue.runtime_value -= value;
            return intValue;
        }

        public static Runtime_IntValue operator +(Runtime_IntValue intValue, int value) {
            intValue.runtime_value += value;
            return intValue;
        }

        public static bool operator >(Runtime_IntValue intValue, float value) => intValue.runtime_value > value;

        public static bool operator >=(Runtime_IntValue intValue, float value) => intValue.runtime_value >= value;

        public static bool operator <(Runtime_IntValue intValue, float value) => intValue.runtime_value < value;

        public static bool operator <=(Runtime_IntValue intValue, float value) => intValue.runtime_value <= value;
        
        public void OnBeforeSerialize() { }
        public void OnAfterDeserialize() => runtime_value = initial_value;

        public void Decrease(int in_val) => runtime_value -= in_val;
    }
}