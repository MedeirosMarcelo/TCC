using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Right
{
    public class RightRecover : AttackState
    {
        public RightRecover(AttackFsm fsm) : base(fsm)
        {
            Name = "RIGHTRECOVER";
            totalTime = 0.2f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("RightRecover");
        }

        public override void PreUpdate()
        {
            if (elapsed >= totalTime)
            {
                Fsm.Fsm.ChangeState("MOVEMENT");
            }
        }
    }
}
