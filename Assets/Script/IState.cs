using Script.Mono.AI;
using Script.So.AI;

namespace Script {
    public interface IState {
        public void DoActions(AI_Brain in_self);
        public void UpdateState(AI_Brain in_self);
        public void DecideTransition(AI_Brain in_self);
    }
}
