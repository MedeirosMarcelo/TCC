namespace Assets.Scripts.Character.States.Attack
{
    using InputEvent = Input.InputEvent;

    public abstract class BaseWindUp : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public BaseWindUp(CharacterFsm fsm) : base(fsm)
        {
            timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.Animator);
            turnRate = 0f;
        }

        public override void PreUpdate()
        {
            if (Input.buffer.NextEventIs<InputEvent.Block>())
            {
                if (Character.Stance == SwordStance.High)
                {
                    var evt = Input.buffer.Pop<InputEvent.Block>();
                    Fsm.ChangeState("BLOCK/HIGH/WINDUP", 0f, evt);
                }
                else
                {
                    var evt = Input.buffer.Pop<InputEvent.Block>();
                    Fsm.ChangeState("BLOCK/MID/WINDUP", 0f, evt);
                }
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

