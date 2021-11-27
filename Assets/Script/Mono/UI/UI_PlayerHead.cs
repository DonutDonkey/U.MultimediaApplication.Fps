using System;
using Script.So;
using UnityEngine;

namespace Script.Mono.UI {
    public class UI_PlayerHead : MonoBehaviour {
        [Header("Data refferences")]
        [SerializeField] private Material mat_player_hp_100;
        [SerializeField] private Material mat_player_hp_50;
        [SerializeField] private Material mat_player_hp_20;
        [SerializeField] private Material mat_player_hp_0;

        [SerializeField] private Runtime_IntValue r_player_hp;

        [Header("Object refferences")]
        [SerializeField] private SkinnedMeshRenderer mesh_renderer;

        private void Update() => mesh_renderer.material = GetPlayerBopMat();

        private Material GetPlayerBopMat() {
            if (r_player_hp.runtime_value <= 0)
                return mat_player_hp_0;
            if (r_player_hp.runtime_value <= 20)
                return mat_player_hp_20;
            if (r_player_hp.runtime_value <= 50)
                return mat_player_hp_50;
            return mat_player_hp_100;
        }
    }
}
