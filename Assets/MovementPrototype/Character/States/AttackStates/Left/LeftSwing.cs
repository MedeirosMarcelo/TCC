using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Left
{
    public class LeftSwing : AttackState
    {
        const float speed = 2f;
        public LeftSwing(AttackFsm fsm) : base(fsm)
        {
            Name = "LEFT/SWING";
            nextState = "LEFT/RECOVER";
            totalTime = 0.1f;
            damage = 1;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("Swing");
            Character.swordTrail.Activate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }
    }
}
