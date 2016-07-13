namespace Assets.Scripts.Character.States
{
    public class Stand : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }

        public Stand(CharacterFsm fsm) : base(fsm)
        {
            Name = "STAND";
            turnRate = 0f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = 3.733f;
            timer.OnFinish = () => {
                timer.OnFinish = () => Fsm.ChangeState("RETURN");
            };

            animation = new AnimationBehaviour(this, Character.Animator);
            animation.TotalTime = 3.733f;
            animation.PlayTime = 3.733f;
            animation.Name = "Stand";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.DropSword();
        }
    }
}