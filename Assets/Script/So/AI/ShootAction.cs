using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu(menuName = "AI/Actions/Shoot", order = 0)]
    public class ShootAction : Action
    {
        public override void Act(AI_Brain brain)
        {
            Shoot(brain);
        }

        private void Shoot(AI_Brain brain)
        {
            if (brain.CheckIfCountDownElapsed(5.05f))
            {
                Debug.Log($"I shoot now!!!");
            }
        }
    }
}