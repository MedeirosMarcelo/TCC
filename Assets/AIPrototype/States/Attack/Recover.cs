namespace Assets.AIPrototype.States.Attack
{
    public class Recover : MinionState
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;
        public Recover(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK/RECOVER";
            staminaCost = 0.01f; // uses 5% of stamina each attack/recover
            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.55f;
            timer.OnFinish = () => NextState();

            animation = new AnimationBehaviour(this, Animator);
            animation.AnimationTime = 0.55f;
            animation.Animation = "LeftRecover";
        }
    }
}
