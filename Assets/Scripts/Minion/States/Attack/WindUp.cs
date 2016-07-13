namespace Assets.Scripts.Minion.States.Attack
{
    public class WindUp : MinionState
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;
        public WindUp(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK/WINDUP";
            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.45f;
            timer.OnFinish = () => Fsm.ChangeState("ATTACK/SWING");

            animation = new AnimationBehaviour(this, Animator);
            animation.PlayTime = 0.6f;
            animation.TotalTime = 0.6f;
            animation.Name = "AttackHorizontalWindup";
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
    }
}
