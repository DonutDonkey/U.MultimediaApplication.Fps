using Script.Mono.Actors;
using UnityEngine;

namespace Script.Mono {
    public class LevelCounter : MonoBehaviour {
        public int kill_count = 0;
        public int max_kill_count;

        private void Awake() => kill_count = 0;

        private void Start() => max_kill_count = FindObjectsOfType<Enemy>().Length;

        public void AddToCount() => kill_count += 1;
    }
}