namespace Assets.Scripts.Minion.States.Attack
{
    public class WindUp : MinionState
    {
        TimerBehaviour timer;
        AnimationBehaviour animation;
        public WindUp(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK/WINDUP";
            staminaCost = 0.10f; // uses 10% of stamina each attack/windup
            timer = new TimerBehaviour(this);
            timer.TotalTime = 0.35f;
            timer.OnFinish = () => Fsm.ChangeState("ATTACK/SWING");

            animation = new AnimationBehaviour(this, Animator);
            animation.TotalTime = 0.35f;
            animation.Name = "LeftWindup";
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
    }
}
