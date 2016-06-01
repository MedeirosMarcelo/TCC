namespace Assets.Scripts.Minion.States
{
    public class Advance : MinionState
    {
        TimerBehaviour timer;
        public Advance(MinionFsm fsm) : base(fsm)
        {
            Name = "ADVANCE";
            staminaCost = 0.1f; // uses 10% of stamina each follow second
            timer = new TimerBehaviour(this);
            timer.TotalTime = 1f;
            timer.OnFinish = () => NextState();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            UpdateDestination();
            Look();
        }
        void UpdateDestination()
        {
            Minion.SetDestination(Target.Transform.position, updateRotation: false);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
        }
    }
}