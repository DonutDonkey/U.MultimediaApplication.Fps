using UnityEngine;

namespace Script {
    public class Helper_ControllerPlayer {
        private CharacterController char_ctrl;
        
        public Helper_ControllerPlayer(CharacterController charCtrl) => char_ctrl = charCtrl;

        public Vector3 GetMotion() =>
            char_ctrl.transform.right * GetHorizontalMovement() +
            char_ctrl.transform.forward * GetVerticalMovement();
        
        public float GetVelocity(Vector3 velocity) => (char_ctrl.isGrounded) 
            ? 0f 
            : velocity.y + World_Constants.GRAVITY * Time.deltaTime;

        public float GetAirVelocity(Vector3 velocity, float jumpForce) =>
            IsJumping() && char_ctrl.isGrounded ? Mathf.Sqrt(jumpForce * -2f * World_Constants.GRAVITY) : velocity.y;
        
        public float GetHorizontalMovement() => Input.GetAxis(World_Constants.AXIS_NAME_HORIZONTAL);
        public float GetVerticalMovement() => Input.GetAxis(World_Constants.AXIS_NAME_VERTICAL);
        
        public bool IsJumping() => Input.GetButton(World_Constants.JUMP_NAME_INPUT);
    }
}
