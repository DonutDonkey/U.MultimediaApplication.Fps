using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Containers/Settings")]
    public class GameSettings : SingletonScriptableObj<GameSettings> {
        public string some;
    }
}