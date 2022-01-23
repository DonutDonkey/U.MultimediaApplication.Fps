using UnityEngine.SceneManagement;

namespace Script.Mono.Managers {
    public class SceneLoader : Singleton<SceneLoader> {
        public static string next_scene_name;
        
        public void LoadNextScene(string in_scene) => SceneManager.LoadScene(in_scene);
    }
}