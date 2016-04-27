using UnityEngine;

namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class DashDeccel : CharState
    {
        public DashDeccel(CharFsm fsm) : base(fsm)
        {
            Name = "DASH/DECCEL";
            totalTime = 0.04f;
            nextState = "DASH/RECOVER";
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 finalVelocity = Character.DashVelocity * (1 - (elapsed / totalTime));
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.ApplyBaseMaterial();
        }
    }
}
