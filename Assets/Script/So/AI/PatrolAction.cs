using System.Collections;
using System.Collections.Generic;
using Script.Mono.AI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Script.So.AI
{
    [CreateAssetMenu(menuName = "AI/Actions/Patrol", order = 0)]
    public class PatrolAction : Action
    {
        public override void Act(AI_Brain brain)
        {
            Debug.Log(brain.ACurrentPoi.GetTransform().gameObject.name);
            Patrol(brain);
        }

        private void Patrol(AI_Brain brain)
        {
            brain.SetAnimationState("patrol");
            NavMeshAgent agent = brain.agent;
            GameObject[] poiArr = brain.PoiIdleWalkPoints;

            if (!agent.hasPath)
            {
                agent.SetDestination(poiArr[0].transform.position);
                brain.nextWaypoint = 1;
            }
            else
            {
                if (agent.remainingDistance < 0.1)
                {
                    agent.SetDestination(poiArr[brain.nextWaypoint].transform.position);
                    brain.nextWaypoint = (brain.nextWaypoint + 1) % poiArr.Length;
                }
            }
        }
    }
}