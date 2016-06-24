namespace Assets.Scripts.Character.States.Attack
{
    public abstract class BaseRecover : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public BaseRecover(CharacterFsm fsm) : base(fsm)
        {
            timer = new TimerBehaviour(this);
            animation = new AnimationBehaviour(this, Character.Animator);
            turnRate = 0f;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.SwordTrail.Deactivate();
        }
    }
}