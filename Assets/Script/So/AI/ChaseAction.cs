using Script.Mono.AI;
using Script.Mono.Managers;
using UnityEngine;

namespace Script.So.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Chase", order = 0)]
    public class ChaseAction : Action
    {
        public override void Act(AI_Brain brain)
        {
            float random_jitter = Random.Range(0.1f, 0.3f);
            if (!brain.CheckIfCountDownElapsed(5 + random_jitter))
            {
                if (isShootDistance(brain))
                {
                    Stop(brain);
                    Aim(brain);
                    Shoot(brain);
                }
                else Chase(brain);
                
            }
            else
            {
                if (!brain.CheckIfCountDownElapsed(6.5f - random_jitter))
                {
                    Stop(brain);
                    Aim(brain);
                    Shoot(brain);
                }
                else brain.stateTimeElapsed = 0;
            }
        }
    
        private void Chase(AI_Brain brain) => brain.agent.SetDestination(brain.ACurrentPoi.GetPosition());
        private void Stop(AI_Brain brain) => brain.agent.ResetPath();

        private void Aim(AI_Brain brain) => brain.transform.LookAt(
            new Vector3(brain.ACurrentPoi.GetPosition().x, brain.transform.position.y, brain.ACurrentPoi.GetPosition().z),
            Vector3.up);

        private bool isShootDistance(AI_Brain brain) =>
            Vector3.Distance(brain.ASelf.GetPosition(), brain.ACurrentPoi.GetPosition()) < 10;
        private void Shoot(AI_Brain brain)
        {
            Debug.Log($"I shoot now!!!");
            var projectile = Manager_Pooler.Instance.PoolEnemyProjectile1.Get();
            projectile.PosProjectileActorPosition = brain.transform;
            projectile.transform.position = brain.transform.position;
            projectile.transform.forward = brain.transform.forward;
                
            projectile.gameObject.SetActive(true);
        }
    }
}