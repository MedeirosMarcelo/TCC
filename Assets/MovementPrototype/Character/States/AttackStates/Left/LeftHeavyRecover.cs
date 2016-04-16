using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Left
{
    public class LeftHeavyRecover : AttackState
    {
        public LeftHeavyRecover(AttackFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";
            totalTime = 0.3f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("Recover");
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.swordTrail.Deactivate();
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
