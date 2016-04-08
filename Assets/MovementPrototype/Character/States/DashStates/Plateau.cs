using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Plateau : DashState
    {
        Vector3 velocity;
        public Plateau(DashFsm fsm) : base(fsm)
        {
            Name = "PLATEAU";
            totalTime = 0.04f;
            nextState = "DECCEL";
        }

        public override void Enter(StateTransitionArgs args)
        {
            base.Enter(args);
            velocity = DashFsm.Velocity;
        }

        public override void Update()
        {
            base.Update();
            Character.Move(Transform.position + velocity * Time.fixedDeltaTime);
        }
    }
}
