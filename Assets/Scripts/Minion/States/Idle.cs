namespace Assets.Scripts.Minion.States
{
    public class Idle : MinionState
    {
        TimerBehaviour timer;
        public Idle(MinionFsm fsm) : base(fsm)
        {
            Name = "IDLE";
            timer = new TimerBehaviour(this);
            staminaCost = -0.5f; // recovers 5% of stamina each idle
            timer.TotalTime = 0.5f;
            timer.OnFinish = () => NextState();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
            Minion.Animator.CrossFade("MoveLow", 0.3f, -1, 0);
        }
    }
}