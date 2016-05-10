using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionIdle : MinionState
    {
        public MinionIdle(MinionFsm fsm) : base(fsm)
        {
            Name = "IDLE";
            nextState = "WANDER";
            totalTime = 2f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = (Random.value > 0.2) ? "FOLLOW" : "WANDER";
        }
    }
}