using Script.Mono.AI;
using UnityEngine;

namespace Script.So.AI {
    [CreateAssetMenu(fileName = "STATE", menuName = "AI/State", order = 0)]
    public class State : ScriptableObject, IState
    {
        public Action[] actions;
        public Transition[] transitions;
        public void DoActions(AI_Brain in_self) {
            foreach (var action in actions) 
                action.Act(in_self);
        }

        public async void UpdateState(AI_Brain in_self) {
            DecideTransition (in_self);
            DoActions (in_self);
        }

        public void DecideTransition(AI_Brain in_self) {
            foreach (var transition in transitions)
            {
                bool decided = transition.decision.Decide(in_self);
                
                in_self.TransitionToState(decided ? transition.trueState : transition.falseState);
                in_self.remainState = decided ? transition.trueState : transition.falseState;

                if (decided) return;
            }
        }
    }
}
