using Script.Mono.Actors;
using UnityEngine;

// use abstract class since conditions need to be custom made ?
namespace Script.So.AI {
    [CreateAssetMenu(fileName = "FieldOfView Condition", menuName = "AI/Conditions/Fov", order = 0)]
    public class AI_C_FieldOfView : ScriptableObject, ICondition {
        public bool Check(IActor in_self, IActor in_poi) => CheckFieldOfView(in_self.GetTransform(), in_poi.GetTransform());

        //DIRTY just to showcase
        private bool CheckFieldOfView(Transform in_self, Transform in_poi) {
            var angle = in_self.GetComponent<Enemy>().DActor.FOV;
            return Vector3.Angle(in_self.position, (in_poi.position - in_self.position).normalized) < angle / 2;
        }
    }
}