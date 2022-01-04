using System.Collections;
using UnityEngine;

namespace Script.Mono.Actors.Player {
    public class Controller_Player : MonoBehaviour, IKnockbackable {
        [Header("Component refferences")]
        [SerializeField] private CharacterController plr_ctrl;
        
        private Helper_ControllerPlayer plr_ctrl_helper;
        private Vector3 velocity;

        private float speed = 10f;
        private float speed_max = 20f;
        private float jump_strength = 2f;

        private float knockback_counter = 0f;
        
        private void Awake() => plr_ctrl_helper = new Helper_ControllerPlayer(plr_ctrl);
        private void Start() => Cursor.lockState = CursorLockMode.Locked; //TODO : fix

        private bool prev_is_air = false;
        private bool prev_is_decreasing = false;

        private void CheckSpeedIncrease() {
            if (!plr_ctrl.isGrounded) return;
            if (!plr_ctrl_helper.IsJumping()) return;
            
            prev_is_air  = false;
            speed += (plr_ctrl_helper.GetMotion() != Vector3.zero) ? 1 : 0f;
        }

        private void CheckSpeedDecrease() {
            if (plr_ctrl_helper.IsJumping()) {
                prev_is_air = true;          
            } else if (!prev_is_decreasing) {
                prev_is_decreasing = true;
                StartCoroutine(BunnyHopSpeedDecrease());
            }
        }

        private IEnumerator BunnyHopSpeedDecrease() {
            while (10f < speed && prev_is_decreasing) {
                speed--;
                yield return new WaitForSeconds(0.01f);
            }
            prev_is_decreasing = false;
        }

        private void Update() {
            CheckSpeedIncrease();
            var grounded = prev_is_air;
    
            
            var motion = plr_ctrl_helper.GetMotion();

            velocity.y = plr_ctrl_helper.GetVelocity(velocity);
            velocity.y = plr_ctrl_helper.GetAirVelocity(velocity, jump_strength);

            // if grounded
            // GroundMove
            //Else
            // AirMove
            // move Velocity * Time.deltaTime
            plr_ctrl.Move(motion * speed * Time.deltaTime);


            knockback_counter -= knockback_counter >= 0 ? Time.deltaTime : 0;
            //Also check if hit the wall or something simmilair? but gravity should do the trick
            if (knockback_counter <= 0 &&
                Physics.CheckSphere(transform.position, 0.4f, LayerMask.GetMask("Environment"))) {
                velocity.x = 0f;
                velocity.z = 0f;
            }

            plr_ctrl.Move(velocity * Time.deltaTime);
            CheckSpeedDecrease();
        }

        // http://adrianb.io/2015/02/14/bunnyhop.html
        // https://github.com/IsaiahKelly/quake3-movement-for-unity/blob/master/Quake3Movement/Scripts/Q3PlayerController.cs
        private void GroundMove(Vector3 motion) {
            // m_MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            var wishDir = new Vector3(motion.x, 0, motion.z);
            wishDir = transform.TransformDirection(wishDir);
            wishDir.Normalize();

            var wishspeed = wishDir.magnitude;
            
            // wishspeed *= MaxSpeed;
            // Prev Velocity is our Velocity member in class sp slo[ om jere
            // ground_accelerate and max_velocity_ground are server-defined movement variables
            // return Accelerate(accelDir, prevVelocity, ground_accelerate, max_velocity_ground);
            // Accelerate(wishDir, wishspeed, m_GroundSettings.Acceleration);
        }

        private void Accelerate(Vector3 target_direction, float accelerate, float max_velocity) {
            float proj_vel = Vector3.Dot(velocity, target_direction);
            // float accel_vel = accelerate - proj_vel;
            float accel_vel = accelerate * Time.deltaTime;
            if (accel_vel <= 0) return;

            if (proj_vel + accel_vel > max_velocity)
                accel_vel = max_velocity - proj_vel;

            velocity = target_direction * accel_vel;
            velocity.x += target_direction.x * accel_vel;
            velocity.z += target_direction.z * accel_vel;
        }

        public void KnockBack(Vector3 other, float force) {
            knockback_counter = 0.5f;
            
            velocity = (transform.position - other).normalized * force;
            velocity.y = force > 10f ? force : velocity.y;
        }
    }
}
