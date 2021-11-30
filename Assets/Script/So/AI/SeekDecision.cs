using System.Collections;
using System.Collections.Generic;
using Script.Mono.Actors;
using Script.Mono.Actors.Player;
using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    [CreateAssetMenu (menuName = "AI/Decisions/Seek")] 
    public class SeekDecision : Decision
    {
        public override bool Decide(AI_Brain brain)
        {
            return Seek(brain);
        }

        private bool Seek(AI_Brain brain)
        {
            bool result = false;
            IActor self = brain.ASelf;
            IActor currentPoi = brain.ACurrentPoi;
            
            Actor actorSelf = brain.GetComponent<Enemy>().DActor;
            var angle = actorSelf.FOV;
            if ((currentPoi.GetPosition() - self.GetPosition()).magnitude < actorSelf.viewRadius)
            {
                result = Vector3.Angle(self.GetTransform().forward, (currentPoi.GetPosition() - self.GetPosition()).normalized) < angle / 2;
            }

            Debug.Log($"SEE TARGET: {result}");
            return result;
        }
    }
}