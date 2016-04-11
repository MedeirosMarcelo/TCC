using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Deccel : DashState
    {
        public Deccel(DashFsm fsm) : base(fsm)
        {
            Name = "DECCEL";
            totalTime = 0.04f;
            nextState = "ENDED";
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 finalVelocity = DashFsm.Velocity * (1 - (elapsed / totalTime));
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }
    }
}
