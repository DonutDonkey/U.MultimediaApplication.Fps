using System;
using System.Collections;
using Script.Mono.Listeners;
using Script.So.Events;
using UnityEngine;

namespace Script.Mono.UI {
    public class UI_DamageIndicator : MonoBehaviour {
        [Header("Refferences")]
        [SerializeField] private SpriteRenderer alpha_hurt;
        [SerializeField] private Transform t_player;

        [Header("Listeners")]
        [SerializeField] private CodeListener<Transform, Event<Transform>> l_ui_hurt_indicator;

        private float timer_enabled;
        private bool is_enabled;

        private void OnEnable() => l_ui_hurt_indicator.OnEnable(HurtIndicator);
        private void OnDisable() => l_ui_hurt_indicator.OnDisable();

        private void Update() {
            timer_enabled += Time.deltaTime;
            if (timer_enabled > 2f) is_enabled = false;
        }

        public void HurtIndicator(Transform t_target) {
            Color c = alpha_hurt.color;
            c.a = 255f;
            alpha_hurt.color = c;
            timer_enabled = 0f;
            is_enabled = true;
            StartCoroutine(RotateToTarget(t_target, c));
        }

        private float time_fade = 0.5f;
        IEnumerator RotateToTarget(Transform t_target, Color in_c) {
            while (is_enabled) {
                var rot = t_target.rotation;
                var direction = t_target.position - t_player.position;
            
                rot = Quaternion.LookRotation(direction);
                rot.z = -rot.y;
                rot.x = 0;
                rot.y = 0;
                
                var north = new Vector3(0, 0, t_player.eulerAngles.y);
                transform.localRotation = rot * Quaternion.Euler(north);
                
                yield return null;
            }
            
            var elapsedTime = 0.0f;
            while (elapsedTime < time_fade) {
                elapsedTime += Time.deltaTime ;
                in_c.a = 1.0f - Mathf.Clamp01(elapsedTime / time_fade);
                alpha_hurt.color = in_c;
                yield return null;
            }
        }
    }
}
