using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "Actor", menuName = "Actors/Actor", order = 0)]
    public class Actor : ScriptableObject {
        public int health;
        public float FOV;
    }
}