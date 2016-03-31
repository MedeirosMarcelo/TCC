using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CIdle : CState
    {
        public CIdle(BaseFsm fsm) : base(fsm)
        {
            Name = "IDLE";
        }

        public override void Enter(StateTransitionEventArgs args)
        {
            FsmPlayer.Character.ChangeVelocity(Vector3.zero);
        }

        public override void Exit()
        {
            FsmPlayer.Character.ChangeVelocity(Vector3.zero);
        }

        public override void PreUpdate()
        {
            if (FsmPlayer.Character.input.dash)
            {
                ChangeState("DASH");
            }
            else if (FsmPlayer.Character.input.move.vector.magnitude > FsmPlayer.Character.input.deadZone)
            {
                ChangeState("WALK");
            }
        }

        public override void Update()
        {
            FsmPlayer.Character.Look();
        }
    }
}