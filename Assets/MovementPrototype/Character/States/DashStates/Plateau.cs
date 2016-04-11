using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Plateau : DashState
    {
        public Plateau(DashFsm fsm) : base(fsm)
        {
            Name = "PLATEAU";
            totalTime = 0.04f;
            nextState = "DECCEL";
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + DashFsm.Velocity * Time.fixedDeltaTime);
        }
    }
}
