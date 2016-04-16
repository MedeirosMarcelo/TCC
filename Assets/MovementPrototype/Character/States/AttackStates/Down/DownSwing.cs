using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Down
{
    public class DownSwing : AttackState
    {
        const float speed = 2f;
        public DownSwing(AttackFsm fsm) : base(fsm)
        {
            Name = "DOWNSWING";
            nextState = "DOWNRECOVER";
            totalTime = 0.1f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("DownSwing");
            Character.Trail.Activate();
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.Trail.Deactivate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }
    }
}
