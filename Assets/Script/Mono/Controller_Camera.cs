using UnityEngine;

namespace Script.Mono {
    public class Controller_Camera : MonoBehaviour {
        [SerializeField] private Transform plr_transform;
        [SerializeField] private Transform cam_transform;
        
        private Helper_ControllerPlayer plr_ctrl_helper;
        private float x_axis_rotation = 0f;
        
        public float offset = 3.32f;
        public float max_angle = 5f;
        public float rate = 10f;
        
        private void Awake() => plr_ctrl_helper = new Helper_ControllerPlayer();

        private void Update() {
            x_axis_rotation -= plr_ctrl_helper.GetMouseY();
            x_axis_rotation = Mathf.Clamp(x_axis_rotation, -90f, 90f);
            
            transform.localRotation = Quaternion.Euler(x_axis_rotation, 0f, 0f);
            plr_transform.Rotate(Vector3.up * plr_ctrl_helper.GetMouseX());

            cam_transform.localRotation = Quaternion.Lerp(
                cam_transform.localRotation,
                Quaternion.Euler(cam_transform.localRotation.x, cam_transform.localRotation.y,
                    plr_ctrl_helper.GetHorizontalMovement() * max_angle),
                Time.deltaTime * rate);

            cam_transform.localPosition = Vector3.Lerp(
                cam_transform.localPosition,
                new Vector3(plr_ctrl_helper.GetHorizontalMovement() / offset, cam_transform.localPosition.y,
                    plr_ctrl_helper.GetVerticalMovement() / offset),
                Time.deltaTime * rate);
        }

        private float timer = 0;
        private void FixedUpdate() {
            var localPosition = cam_transform.localPosition;
            
            if (plr_ctrl_helper.GetAnyMovemet() && !plr_ctrl_helper.IsJumping())
                CamMovementTransform(localPosition);
            else
                CamIdleMovement(localPosition);
        }
        
        private float bop_speed = 10f;
        private void CamMovementTransform(Vector3 localPosition) {
            timer += Time.deltaTime * bop_speed;
            
            cam_transform.localPosition =  new Vector3(
                localPosition.x, 
                0 + Mathf.Sin(timer) * 0.005f, 
                localPosition.z);
        }
        private void CamIdleMovement(Vector3 localPosition) {
            timer = 0f;
            cam_transform.localPosition = new Vector3(localPosition.x,
                Mathf.Lerp(localPosition.y, 0, Time.deltaTime * bop_speed),
                localPosition.z);;
        }
    }
}
