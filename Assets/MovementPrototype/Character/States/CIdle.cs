using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CIdle : CState
    {
        public CIdle(CFsm fsm) : base(fsm)
        {
            Name = "IDLE";
        }

        public override void Enter(StateTransitionArgs args)
        {
            Character.ChangeVelocity(Vector3.zero);
        }

        public override void Exit(StateTransitionArgs args)
        {
            Character.ChangeVelocity(Vector3.zero);
        }

        public override void PreUpdate()
        {
            if (Input.buffer.Peek() == InputEvent.Attack) {
                Input.buffer.Pop();
                Fsm.ChangeState("ATTACK");
                return;
            }
            if (Input.buffer.Peek() == InputEvent.Dash)
            {
                Input.buffer.Pop();
                Fsm.ChangeState("DASH");
                return;
            }
            if (Input.move.isActive)
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