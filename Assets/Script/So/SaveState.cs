using UnityEngine;

namespace Script.So {
    [CreateAssetMenu(fileName = "New Save State", menuName = "new Save State")]
    public class SaveState : ScriptableObject {
        [SerializeField] private PlayerContainer player_container;
        //Current Map Data etc
    }
}