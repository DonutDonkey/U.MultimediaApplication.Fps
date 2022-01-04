using Script.Mono.Managers;
using UnityEngine;

namespace Script.Mono {
    public class Decal : MonoBehaviour {
        private static int _count = 0;
        private float timer = 0;
        
        private void OnEnable() => _count++;
        private void OnDisable() => _count--;

        private void Update() {
            timer = timer > 10 ? 0 : timer + Time.deltaTime;
            
            if (timer > 10 || _count > 50)
                Manager_Pooler.Instance.Pool_hitscan_decal.Release(this);
        }
    }
}
