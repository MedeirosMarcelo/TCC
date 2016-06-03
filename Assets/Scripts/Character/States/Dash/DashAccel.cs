using UnityEngine;

namespace Assets.Scripts.Character.States.Dash
{
    public class DashAccel : CharacterState
    {
        public DashAccel(CharacterFsm fsm) : base(fsm)
        {
            Name = "DASH/ACCEL";
            nextState = "DASH/PLATEAU";
            totalTime = 0.04f;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Vector3 finalVelocity = Character.DashVelocity * (elapsed / totalTime);
            Character.Move(Transform.position + finalVelocity * Time.fixedDeltaTime);
        }
    }
}
