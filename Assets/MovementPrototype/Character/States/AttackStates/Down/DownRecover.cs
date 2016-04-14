using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Down
{
    public class DownRecover : AttackState
    {
        public DownRecover(AttackFsm fsm) : base(fsm)
        {
            Name = "DOWNRECOVER";
            totalTime = 0.2f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("DownRecover");
        }

        public override void PreUpdate()
        {
            if (elapsed >= totalTime)
            {
                Fsm.Fsm.ChangeState("IDLE");
            }
        }
    }
}
