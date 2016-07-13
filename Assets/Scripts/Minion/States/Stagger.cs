namespace Assets.Scripts.Minion.States
{
    public class Stagger : MinionState 
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;

        public Stagger(MinionFsm fsm) : base(fsm)
        {
            Name = "STAGGER";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 1.2f;
            timer.OnFinish = () => Fsm.ChangeState("IDLE");

            animation = new AnimationBehaviour(this, Animator);
            animation.PlayTime =  1.2f;
            animation.TotalTime = 1.2f;
            animation.Name = "Stagger";
        }
    }
}