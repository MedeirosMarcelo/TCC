using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class DashAccel : CharState
    {
        public DashAccel(CharFsm fsm) : base(fsm)
        {
            Name = "DASH/ACCEL";
            nextState = "DASH/PLATEAU";
            totalTime = 0.04f;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Look();
            Vector3 finalVelocity = Character.DashVelocity * (elapsed / totalTime);
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.ApplyDodgeMaterial();
        }
    }
}
