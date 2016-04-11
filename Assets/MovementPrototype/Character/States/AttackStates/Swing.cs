using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class Swing : AttackState 
    {
        const float speed = 2f;
        public Swing(AttackFsm fsm) : base(fsm)
        {
            Name = "SWING";
            nextState = "RECOVER";
            totalTime = 0.3f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f/totalTime);
            Character.animator.Play("Swing");
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }

    }
}
