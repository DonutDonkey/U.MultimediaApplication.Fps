using UnityEngine;

namespace Script.So
{
    public class Singleton<T> : ScriptableObject where T: ScriptableObject
    {
        private static T instance;

        public static T Instance { get; private set; }
    }
}