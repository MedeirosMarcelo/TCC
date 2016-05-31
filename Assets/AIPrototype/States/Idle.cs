namespace Assets.AIPrototype.States
{
    public class Idle : MinionState
    {
        TimerBehaviour timer;
        public Idle(MinionFsm fsm) : base(fsm)
        {
            Name = "IDLE";
            timer = new TimerBehaviour(this);
            staminaCost = -0.5f; // recovers 5% of stamina each idle
            timer.TotalTime = 3f;
            timer.OnFinish = () => NextState();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
    }
}