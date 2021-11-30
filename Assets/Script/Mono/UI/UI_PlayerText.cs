using Script.So;
using TMPro;
using UnityEngine;

namespace Script.Mono.UI {
    public class UI_PlayerText : MonoBehaviour {
        [SerializeField] private Runtime_IntValue value;
        private TextMeshProUGUI tmp_text;

        private void Awake() => tmp_text = GetComponent<TextMeshProUGUI>();

        private void Update() {
            if (!tmp_text.text.Equals(value.runtime_value.ToString()) && value.runtime_value > 0)
                tmp_text.text = value.runtime_value.ToString();
            else if (value.runtime_value <= 0)
                tmp_text.text = string.Empty;
        }
    }
}
