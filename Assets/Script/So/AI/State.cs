using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu(fileName = "STATE", menuName = "AI/State", order = 0)]
    public class State : ScriptableObject, IState {
        public void Enter(AI_Brain in_self) {
            throw new System.NotImplementedException();
        }

        public void Tick(AI_Brain in_self) {
            throw new System.NotImplementedException();
        }

        public void Exit(AI_Brain in_self) {
            throw new System.NotImplementedException();
        }
        
        private void CheckTransitions() {
            
        }
    }
}