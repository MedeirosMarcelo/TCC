using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class DashPlateau : CharState
    {
        public DashPlateau(CharFsm fsm) : base(fsm)
        {
            Name = "DASH/PLATEAU";
            totalTime = 0.04f;
            nextState = "DASH/DECCEL";
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + Character.DashVelocity * Time.fixedDeltaTime);
        }
    }
}
