using UnityEngine;

namespace Script.Mono.Actors.Player {
    public class Controller_Player : MonoBehaviour, IKnockbackable {
        [Header("Component refferences")]
        [SerializeField] private CharacterController plr_ctrl;
        
        private Helper_ControllerPlayer plr_ctrl_helper;
        private Vector3 velocity;

        private float speed = 10f;
        private float jump_strength = 2f;

        private float knockback_counter = 0f;
        private void Awake() => plr_ctrl_helper = new Helper_ControllerPlayer(plr_ctrl);
        private void Start() => Cursor.lockState = CursorLockMode.Locked; //TODO : fix

        private void Update() {
            var motion = plr_ctrl_helper.GetMotion();

            velocity.y = plr_ctrl_helper.GetVelocity(velocity);
            velocity.y = plr_ctrl_helper.GetAirVelocity(velocity, jump_strength);

            plr_ctrl.Move(motion * speed * Time.deltaTime);


            knockback_counter -= knockback_counter >= 0 ? Time.deltaTime : 0;
            //Also check if hit the wall or something simmilair? but gravity should do the trick
            if (knockback_counter <= 0 &&
                Physics.CheckSphere(transform.position, 0.4f, LayerMask.GetMask("Environment"))) {
                velocity.x = 0f;
                velocity.z = 0f;
            }

            plr_ctrl.Move(velocity * Time.deltaTime);
        }

        public void KnockBack(Vector3 other, float force) {
            knockback_counter = 0.5f;
            
            velocity = (transform.position - other).normalized * force;
            velocity.y = force > 10f ? force : velocity.y;
        }
    }
}
