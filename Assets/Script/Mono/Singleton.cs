using UnityEngine;

namespace Script.Mono {
    public class Singleton<T> : MonoBehaviour where T : Component {
        public static T Instance { get; private set; }

        protected virtual void Awake() {
            if (Instance == null) {
                Debug.Log(GetType().Name + "- Instance Created");
                Instance = this as T;
            } else {
                Debug.Log(GetType().Name + "- Instance Destroyed");
                Destroy(gameObject);
            }
        }
    }
}
