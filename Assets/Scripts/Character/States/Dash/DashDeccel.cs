using UnityEngine;

namespace Assets.Scripts.Character.States.Dash
{
    public class DashDeccel : CharacterState
    {
        public DashDeccel(CharacterFsm fsm) : base(fsm)
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
    }
}
