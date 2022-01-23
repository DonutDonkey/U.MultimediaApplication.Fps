using System;
using TMPro;
using UnityEngine;

namespace Script.Mono.UI {
    public class UI_EndScreen : MonoBehaviour {
        public TextMeshProUGUI TMP_kills;
        public TextMeshProUGUI TMP_total;

        public LevelCounter level_counter;

        private void OnEnable() {
            TMP_kills.text = level_counter.kill_count.ToString();
            TMP_total.text = "/ " + level_counter.max_kill_count.ToString();
        }
    }
}