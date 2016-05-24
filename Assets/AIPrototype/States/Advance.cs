namespace Assets.AIPrototype.States
{
    public class Advance : MinionState
    {
        TimerBehaviour timer;
        public Advance(MinionFsm fsm) : base(fsm)
        {
            Name = "ADVANCE";
            staminaCost = 0.025f;
            timer = new TimerBehaviour(this);
            timer.TotalTime = 1f;
            timer.OnFinish = () => NextState();
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SetDestination(Target.position);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
        }
    }
}