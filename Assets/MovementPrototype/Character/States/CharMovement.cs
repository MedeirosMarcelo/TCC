namespace Assets.MovementPrototype.Character.States
{
    public class CharMovement : CharState
    {
        public CharMovement(CharFsm fsm) : base(fsm)
        {
            Name = "MOVEMENT";
            canPlayerMove = true;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.Play("Idle");
        }

        public override void PreUpdate()
        {
            if (Input.buffer.NextEventIs<InputEvent.Attack>())
            {
                var evt = Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK", 0f, evt); //"HATTACK/LIGHT/WINDUP"
            }
            else if (Input.buffer.NextEventIs<InputEvent.Lunge>())
            {
                var evt = Input.buffer.Pop<InputEvent.Lunge>();
                Fsm.ChangeState("LUNGE/LIGHT/WINDUP", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.Block>())
            {
                var evt = Input.buffer.Pop<InputEvent.Block>();
                Fsm.ChangeState("BLOCK/MID/WINDUP", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.Dash>())
            {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Fsm.ChangeState("DASH", 0f, evt);
            }
            else
            {
                base.PreUpdate();
            }
        }
    }
}