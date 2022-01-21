using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu (menuName = "AI/Decisions/Dead")] 
    public class DeadDecision : Decision {
        public override bool Decide(AI_Brain brain) => IsDead(brain);

        private bool IsDead(AI_Brain brain) => brain.ASelf.IsDead();
    }
}