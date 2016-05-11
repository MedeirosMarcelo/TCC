using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionIdle : MinionState
    {
        TimerBehaviour timer;
        public MinionIdle(MinionFsm fsm) : base(fsm)
        {
            Name = "IDLE";
            timer = new TimerBehaviour(this);
            timer.TotalTime = 2f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            timer.NextState = (Random.value > 0.2) ? "FOLLOW" : "WANDER";
        }
    }
}