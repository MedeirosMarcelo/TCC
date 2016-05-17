using UnityEngine;

namespace Assets.Scripts.Fuzzy.StateMachine.Behaviour
{
    public class TimerBehaviour : BaseBehaviour
    {
        public float Elapsed { get; protected set; }
        public float TotalTime { get; set; }
        public string NextState { get; set; }
        public TimerBehaviour(IState state) : base(state)
        {
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (Elapsed >= TotalTime)
            {
                State.Fsm.ChangeState(NextState, TotalTime - Elapsed);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Elapsed += Time.fixedDeltaTime;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Elapsed = additionalDeltaTime;
        }
    }
}
