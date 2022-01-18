using Script.So;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono.Managers {
    public class GameManager : Mono.Singleton<GameManager> {
        [SerializeField] private E_String string_event;
        [SerializeField] private PlayerContainer player_container;

        // Put in Start so scene loading is triggered as last thing, and other managers can be initialized as game persistent singletons
        private void Start() {
            player_container = PlayerContainer.Instance;
            string_event.Invoke("MENU");
            // Raise load scene event for going to first scene from preload
            // M_SceneLoader.next_scene_name = "test_scene";
            // string_event.Raise(M_SceneLoader.next_scene_name);
        }
    }
}