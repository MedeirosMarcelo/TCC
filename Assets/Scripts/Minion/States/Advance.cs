namespace Assets.Scripts.Minion.States
{
    public class Advance : MinionState
    {
        TimerBehaviour timer;
        public Advance(MinionFsm fsm) : base(fsm)
        {
            Name = "ADVANCE";
            timer = new TimerBehaviour(this);
            timer.TotalTime = 1f;
            timer.OnFinish = () => NextState();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
            UpdateDestination();
        }
        void UpdateDestination()
        {
            Minion.NavmeshMove(Target.Transform.position, updateRotation: false);
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Animator.CrossFade("MoveLow", 0.3f, -1, 0);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
        }
    }
}