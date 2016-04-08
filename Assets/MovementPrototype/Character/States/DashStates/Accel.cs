using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Accel : DashState
    {
        Vector3 velocity;
        public Accel(DashFsm fsm) : base(fsm)
        {
            Name = "ACCEL";
            timePercent = 0.1f;
            totalTime = DashFsm.dashTime * timePercent;
            nextState = "PLATEAU";
        }

        public override void Enter(StateTransitionArgs args)
        {
            base.Enter(args);
            velocity = DashFsm.Velocity;
        }

        public override void Update() // should we rename this to fixedUpdate?
        {
            base.Update();
            Vector3 finalVelocity = velocity * (elapsed / totalTime);
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }

    }
}
