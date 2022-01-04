using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Chase", order = 0)]
    public class ChaseAction : Action
    {
        public override void Act(AI_Brain brain)
        {
            Chase(brain);
        }

        private void Chase(AI_Brain brain)
        {
            brain.agent.SetDestination(brain.ACurrentPoi.GetPosition());
        }
    }
}