using Script.Mono.AI;

namespace Script {
    public interface IState {
        public void Enter(AI_Brain in_self);
        public void Tick(AI_Brain in_self);
        public void Exit(AI_Brain in_self);
    }
}
