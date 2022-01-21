using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu(menuName = "AI/Actions/Dead", order = 0)]
    public class DeadAction : Action {
        public override void Act(AI_Brain brain) => Dead(brain);

        private void Dead(AI_Brain brain) {
            brain.SetAnimationState("death");
            if (!brain.event_done) {
                brain.e_death.Invoke();
                brain.event_done = true; // cause otherwise will infinitly addup xd
            }
        }
    }
}