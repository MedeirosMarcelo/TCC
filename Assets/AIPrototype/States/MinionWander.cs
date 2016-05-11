using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionWander : MinionState
    {
        TimerBehaviour timer;
        public MinionWander(MinionFsm fsm) : base(fsm)
        {
            Name = "WANDER";
            timer = new TimerBehaviour(this);
            timer.TotalTime = 2f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            timer.NextState = (Random.value > 0.5) ? "IDLE" : "WANDER";
            NextDestination();
        }
        const float nextRandom = 1.5f;
        void NextDestination()
        {
            Minion.Destination = Transform.position + (Random.insideUnitSphere * nextRandom);
        }
    }
}