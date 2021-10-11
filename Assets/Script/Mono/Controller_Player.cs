using UnityEngine;

namespace Script.Mono {
    public class Controller_Player : MonoBehaviour, IKnockbackable {
        //TODO: Make it rigid body so forces of knockback can be applied, also implement bunny hopping
        private CharacterController char_ctrl;
        private Helper_ControllerPlayer pctrl_helper;

        private Vector3 velocity;

        private float speed = 10f;
        private float jump_strength = 10f;
        
        private void Awake() {
            char_ctrl = GetComponent<CharacterController>();
            pctrl_helper = new Helper_ControllerPlayer(char_ctrl);
        }

        private void Update() {
            Debug.Log(char_ctrl.isGrounded);
            var motion = pctrl_helper.GetMotion();

            velocity.y = pctrl_helper.GetVelocity(velocity);
            velocity.y = pctrl_helper.GetAirVelocity(velocity, jump_strength);

            char_ctrl.Move(motion * (speed * Time.deltaTime));
            char_ctrl.Move(velocity * Time.deltaTime);
        }

        public void KnockBack(float force) {
            velocity = -char_ctrl.transform.forward * force;
        }
    }
}
