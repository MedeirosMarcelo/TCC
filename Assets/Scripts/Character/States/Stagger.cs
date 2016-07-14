namespace Assets.Scripts.Character.States
{
    public class Stagger : CharacterState 
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;

        public Stagger(CharacterFsm fsm) : base(fsm)
        {
            Name = "STAGGER";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 1.3f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");

            animation = new AnimationBehaviour(this, Character.Animator);
            animation.PlayTime =  2f;
            animation.TotalTime = 2f;
            animation.Name = "Stagger";
        }
    }
}