using Script.So;
using UnityEngine;

namespace Script.Mono.Managers {
    public class DataManager : Singleton<DataManager> {
        [SerializeField] private PlayerContainer player_container;

        protected override void Awake() {
            base.Awake();

            player_container = PlayerContainer.Instance;
        }
    }
}