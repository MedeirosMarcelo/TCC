using UnityEngine;

namespace Assets.Scripts.Character.States.Dash
{
    public class DashPlateau : CharacterState
    {
        public DashPlateau(CharacterFsm fsm) : base(fsm)
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
