using System.Collections;
using System.Collections.Generic;
using Script.Mono.AI;
using UnityEngine;
namespace Script.So.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Aware", order = 0)]
    public class AwareAction : Action
    {
        public override void Act(AI_Brain brain)
        {
            Scan(brain);
        }

        private bool Scan(AI_Brain brain)
        {
            Debug.Log($"BEING AWARE");
            brain.agent.isStopped = true;
            brain.ASelf.GetTransform().Rotate (0, 10 * Time.deltaTime, 0);
            return brain.CheckIfCountDownElapsed (10);
        }
    }
}