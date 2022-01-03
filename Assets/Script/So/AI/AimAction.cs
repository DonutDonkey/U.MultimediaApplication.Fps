using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu(menuName = "AI/Actions/Aim", order = 0)]
    public class AimAction : Action
    {
        public override void Act(AI_Brain brain) => Aim(brain);

        private void Aim(AI_Brain brain) => brain.transform.LookAt(brain.ACurrentPoi.GetPosition(), Vector3.up);
    }
}