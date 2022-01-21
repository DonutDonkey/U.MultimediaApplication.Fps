using System;
using System.Threading.Tasks;
using Script.Mono.Actors;
using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI
{
    [CreateAssetMenu (menuName = "AI/Decisions/Hear")] 
    public class HearDecision : Decision
    {
        public override bool Decide(AI_Brain brain)
        {
            return Hear(brain);
        }

        private bool Hear(AI_Brain brain)
        {
            bool result = false;
            IActor self = brain.ASelf;
            IActor currentPoi = brain.ACurrentPoi;
            
            Actor actorSelf = brain.GetComponent<Enemy>().DActor;
            var hearingRadius = 5;
            if ((currentPoi.GetPosition() - self.GetPosition()).magnitude < hearingRadius)
            {
                //TODO wait between position reads
                result = true;
            }

            Debug.Log($"HEAR TARGET: {result}");
            return result;
        }
        private async Task WaitOneSecondAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            Debug.Log("Finished waiting.");
        }
        
    }
}