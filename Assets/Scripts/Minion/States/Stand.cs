namespace Assets.Scripts.Minion.States
{
    public class Stand : MinionState  
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }

        public Stand(MinionFsm fsm) : base(fsm)
        {
            Name = "STAND";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 3.733f;
            timer.OnFinish = () => {
                timer.OnFinish = () => Fsm.ChangeState("SMALLRETURN");
            };

            animation = new AnimationBehaviour(this, Minion.Animator);
            animation.TotalTime = 3.733f;
            animation.PlayTime = 3.733f;
            animation.Name = "Stand";
        }
    }
}