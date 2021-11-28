using UnityEngine;

namespace Script {
    public class Helper_ControllerPlayer {
        private CharacterController char_ctrl;
        
        public Helper_ControllerPlayer(CharacterController charCtrl) => char_ctrl = charCtrl;
        public Helper_ControllerPlayer() { }

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
        public float GetAnyMovemet() => Input.GetAxis(World_Constants.AXIS_NAME_HORIZONTAL) +
                                        Input.GetAxis(World_Constants.AXIS_NAME_VERTICAL);
        public float GetMouseX() => Input.GetAxis(World_Constants.MOUSE_AXIS_X) * Time.deltaTime * 100f; //add mouse sensitivity
        public float GetMouseY() => Input.GetAxis(World_Constants.MOUSE_AXIS_Y) * Time.deltaTime * 100f; //add mouse sensitivity
        
        
        public bool IsJumping() => Input.GetButton(World_Constants.JUMP_NAME_INPUT);
    }
}
