using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Right
{
    public class RightWindUp : AttackState
    {
        const float speed = 1f;
        public RightWindUp(AttackFsm fsm) : base(fsm)
        {
            Name = "RIGHTWINDUP";
            nextState = "RIGHTSWING";
            totalTime = 0.2f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("RightWindup");
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }
    }
}
