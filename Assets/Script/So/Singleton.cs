using UnityEngine;

namespace Script.So
{
    public class Singleton<T> : ScriptableObject where T: ScriptableObject
    {
        public static T Instance { get; private set; }
    }
}