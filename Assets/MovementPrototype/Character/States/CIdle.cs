using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CIdle : CState
    {
        public CIdle(CFsm fsm) : base(fsm)
        {
            Name = "IDLE";
        }

        public override void Enter(StateTransitionEventArgs args)
        {
            Character.ChangeVelocity(Vector3.zero);
        }

        public override void Exit()
        {
            Character.ChangeVelocity(Vector3.zero);
        }

        public override void PreUpdate()
        {
            if (Input.dash)
            {
                Fsm.ChangeState("DASH");
                return;
            }
            if (Input.move.vector.magnitude > Input.deadZone)
            {
                Fsm.ChangeState("WALK");
                return;
            }
        }

        public override void Update()
        {
            Character.Look();
        }
    }
}