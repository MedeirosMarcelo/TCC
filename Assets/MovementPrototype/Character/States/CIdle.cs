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
            if (Input.buffer.NextEventIs<InputEvent.Attack>()) {
                Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK");
                return;
            }
            if (Input.buffer.NextEventIs<InputEvent.Block>()) {
                Input.buffer.Pop<InputEvent.Block>();
                Fsm.ChangeState("BLOCK");
                return;
            }

            if (Input.buffer.NextEventIs<InputEvent.Dash>())
            {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Fsm.ChangeState(new DashTransitionArgs(Name, "DASH", 0f, evt));
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