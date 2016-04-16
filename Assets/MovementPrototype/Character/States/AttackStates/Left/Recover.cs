using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Left
{
    public class Recover : AttackState
    {
        public Recover(AttackFsm fsm) : base(fsm)
        {
            Name = "RECOVER";
            totalTime = 0.2f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("Recover");
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
