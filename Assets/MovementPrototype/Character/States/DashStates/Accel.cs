using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Accel : DashState
    {
        public Accel(DashFsm fsm) : base(fsm)
        {
            Name = "ACCEL";
            nextState = "PLATEAU";
            totalTime = 0.04f;
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Vector3 finalVelocity = DashFsm.Velocity * (elapsed / totalTime);
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }

    }
}
