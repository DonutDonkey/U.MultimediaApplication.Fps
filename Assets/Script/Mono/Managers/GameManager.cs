using Script.So.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Mono.Managers {
    public class GameManager : Mono.Singleton<GameManager> {
        [SerializeField] private E_String string_event;
        
        private void Start() {
            
            // Raise load scene event for going to first scene from preload
            // M_SceneLoader.next_scene_name = "test_scene";
            // string_event.Raise(M_SceneLoader.next_scene_name);
        }
    }
}