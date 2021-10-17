using UnityEngine;

namespace Script.Mono {
    public class Controller_Player : MonoBehaviour, IKnockbackable {
        //TODO: Make it rigid body so forces of knockback can be applied, also implement bunny hopping
        [SerializeField] private CharacterController plr_ctrl;

        private Helper_ControllerPlayer plr_ctrl_helper;
        private Vector3 velocity;

        private float speed = 10f;
        private float speed_sprint = 20f; // no cod, think doom sprint?
        private float jump_strength = 2f;

        private float knockback_counter = 0f;
        private void Awake() => plr_ctrl_helper = new Helper_ControllerPlayer(plr_ctrl);
        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        private void Update() {
            var motion = plr_ctrl_helper.GetMotion();

            velocity.y = plr_ctrl_helper.GetVelocity(velocity);
            velocity.y = plr_ctrl_helper.GetAirVelocity(velocity, jump_strength);
            
            plr_ctrl.Move(motion * speed * Time.deltaTime);

            knockback_counter -= knockback_counter >=0 ? Time.deltaTime : 0;
            //Also check if hit the wall or something simmilair? but gravity should do the trick
            if (knockback_counter <= 0 && Physics.CheckSphere(transform.position, 0.4f, LayerMask.GetMask("Environment"))) {
                velocity.x = 0f;
                velocity.z = 0f;
            }

            plr_ctrl.Move(velocity * Time.deltaTime);
        }

        public void KnockBack(float force) {
            knockback_counter = 0.5f;
            // direction is this transform - other transform for later
            velocity = -plr_ctrl.transform.forward * force;
            velocity.y = force;
        }
    }
}
