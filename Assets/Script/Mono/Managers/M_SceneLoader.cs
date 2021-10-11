using UnityEngine.SceneManagement;

namespace Script.Mono.Managers {
    public class M_SceneLoader : Singleton<M_SceneLoader> {
        public static string next_scene_name;
        
        public void LoadNextScene(string in_scene) => SceneManager.LoadSceneAsync(in_scene);
    }
}