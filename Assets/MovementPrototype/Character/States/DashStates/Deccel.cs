using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Deccel : DashState
    {
        Vector3 velocity;
        public Deccel(DashFsm fsm) : base(fsm)
        {
            Name = "DECCEL";
            totalTime = 0.04f;
            nextState = "ENDED";
        }

        public override void Enter(StateTransitionArgs args)
        {
            base.Enter(args);
            velocity = DashFsm.Velocity;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 finalVelocity = velocity * (1 - (elapsed / totalTime));
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }
    }
}
