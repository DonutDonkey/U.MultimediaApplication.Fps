using UnityEngine;

namespace Script.Mono {
    public class DDOL : MonoBehaviour {
        private void Awake() => DontDestroyOnLoad(this);
    }
}