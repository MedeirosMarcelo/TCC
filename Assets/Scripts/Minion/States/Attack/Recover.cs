namespace Assets.Scripts.Minion.States.Attack
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
            timer.TotalTime = 0.25f;
            timer.OnFinish = () => NextState();

            animation = new AnimationBehaviour(this, Animator);
            animation.PlayTime = 0.25f;
            animation.TotalTime = 0.5f;
            animation.Name = "AttackHorizontalRecover";
        }
    }
}
