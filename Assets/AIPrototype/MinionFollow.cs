using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionFollow : MinionState
    {
        TimerBehaviour timer;
        public MinionFollow(MinionFsm fsm) : base(fsm)
        {
            Name = "FOLLOW";
            timer = new TimerBehaviour(this);
            timer.NextState = "IDLE";
            timer.TotalTime = 2f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            if (InRange() && (Random.value > 0.2))
            {
                Fsm.ChangeState("ATTACK", additionalDeltaTime);
                return;
            }
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            timer.NextState = (Random.value > 0.2) ? "FOLLOW" : "IDLE";
            NextDestination();
        }
        const float nextRange = 3f;
        const float nextRandom = 1f;
        void NextDestination()
        {
            var diff = (Target.position - Transform.position);
            diff.y = 0f;
            if (diff.magnitude > nextRange)
            {
                diff = diff.normalized * nextRange;
            }
            Minion.Destination = Transform.position + diff + (Random.insideUnitSphere * nextRandom);
        }
        const float targetRange = 1.5f;
        bool InRange()
        {
            var diff = (Target.position - Transform.position);
            diff.y = 0f;
            return (diff.magnitude < targetRange);
        }
    }
}