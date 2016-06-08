namespace Assets.Scripts.Character.States.Attack
{

    public abstract class BaseWindUp : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public BaseWindUp(CharacterFsm fsm) : base(fsm)
        {
            timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.animator);
            turnRate = 0f;
        }
    }
}

