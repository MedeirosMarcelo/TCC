using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class Lock : CharMovement
    {
        float moveSpeed = 3.5f;

        public Lock(CharFsm fsm) : base(fsm)
        {
            Name = "LOCK";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.MovementState = Name;
        }

        public override void PreUpdate()
        {
            if (Character.input.run >= 0.25f)
            {
                Character.fsm.ChangeState("RUN");
            }
            else if (Input.buffer.NextEventIs<InputEvent.Attack>())
            {
                var evt = Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.Lunge>())
            {
                var evt = Input.buffer.Pop<InputEvent.Lunge>();
                Fsm.ChangeState("LUNGE/LIGHT/WINDUP", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.BlockMid>())
            {
                Input.buffer.Pop<InputEvent.BlockMid>();
                Fsm.ChangeState("BLOCK/MID/WINDUP");
            }
            else if (Input.buffer.NextEventIs<InputEvent.BlockHigh>())
            {
                Input.buffer.Pop<InputEvent.BlockHigh>();
                Fsm.ChangeState("BLOCK/HIGH/WINDUP");
            }
            else if (Input.buffer.NextEventIs<InputEvent.Dash>())
            {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Fsm.ChangeState("DASH", 0f, evt);
            }
        }

        public override void FixedUpdate()
        {
            Character.Look(lookTurnRate, lockTurnRate);
            Move(moveSpeed);
        }
    }
}