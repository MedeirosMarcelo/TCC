using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Right
{
    public class RightSwing : AttackState
    {
        const float speed = 2f;
        public RightSwing(AttackFsm fsm) : base(fsm)
        {
            Name = "RIGHTSWING";
            nextState = "RIGHTRECOVER";
            totalTime = 0.3f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("RightSwing");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }
    }
}
