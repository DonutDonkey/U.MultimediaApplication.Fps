using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Int Value", menuName = "Values/Int")]
    public class IntValue : ScriptableObject {
        public int value;

        public static implicit operator int(IntValue intValue) => intValue.value;

        public static IntValue operator -(IntValue intValue, int value) {
            intValue.value -= value;
            return intValue;
        }

        public static IntValue operator +(IntValue intValue, int value) {
            intValue.value += value;
            return intValue;
        }

        public static bool operator >(IntValue intValue, float value) => intValue.value > value;

        public static bool operator >=(IntValue intValue, float value) => intValue.value >= value;

        public static bool operator <(IntValue intValue, float value) => intValue.value < value;

        public static bool operator <=(IntValue intValue, float value) => intValue.value <= value;
    }
}