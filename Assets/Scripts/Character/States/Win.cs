namespace Assets.Scripts.Character.States
{
    public class Win : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public Win(CharacterFsm fsm) : base(fsm)
        {
            Name = "WIN";
            turnRate = 0f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = 1.367f;
            timer.OnFinish = () => Fsm.ChangeState("END");

            animation = new AnimationBehaviour(this, Character.animator);
            animation.TotalTime = 1.367f;
            animation.PlayTime = 1.367f;
            animation.Name = "Win";
        }
    }
}