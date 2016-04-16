using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class Lock : CMovement
    {
        float moveSpeed = 3.5f;

        public Lock(CFsm fsm) : base(fsm)
        {
            Name = "LOCK";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
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
            else if (Input.buffer.NextEventIs<InputEvent.Block>())
            {
                Input.buffer.Pop<InputEvent.Block>();
                Fsm.ChangeState("BLOCK/WINDUP");
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