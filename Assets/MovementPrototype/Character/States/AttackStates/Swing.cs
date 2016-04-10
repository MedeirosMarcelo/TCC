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

        public override void Enter(StateTransitionArgs args)
        {
            base.Enter(args);
            Character.swordAnimator.SetTrigger("Attack");
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));

        }

    }
}
