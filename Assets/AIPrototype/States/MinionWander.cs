using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionWander : MinionState
    {
        MinionAttack state;

        public MinionWander(MinionFsm fsm) : base(fsm)
        {
            Name = "WANDER";
            nextState = "IDLE";
            totalTime = 2f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = (Random.value > 0.5) ? "IDLE" : "WANDER";
            NextDestination();
        }
        const float nextRandom = 1.5f;
        void NextDestination()
        {
            Minion.Destination = Transform.position + (Random.insideUnitSphere * nextRandom);
        }
    }
}