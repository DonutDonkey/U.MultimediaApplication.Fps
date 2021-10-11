using Script.So.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Mono.Managers {
    public class M_GameManager : Mono.Singleton<M_GameManager> {
        [SerializeField] private E_String string_event;
        
        private void Start() {
            
            // Raise load scene event for going to first scene from preload
            // M_SceneLoader.next_scene_name = "test_scene";
            // string_event.Raise(M_SceneLoader.next_scene_name);
        }
    }
}