using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Down
{
    public class DownSwing : AttackState
    {
        const float speed = 2f;
        public DownSwing(AttackFsm fsm) : base(fsm)
        {
            Name = "DOWN/SWING";
            nextState = "DOWN/RECOVER";
            totalTime = 0.1f;
            damage = 1;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("DownSwing");
            Character.SwordTrail.Activate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }
    }
}
