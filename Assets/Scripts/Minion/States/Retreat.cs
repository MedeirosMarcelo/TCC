namespace Assets.Scripts.Minion.States
{
    public class Retreat : MinionState
    {
        TimerBehaviour timer;
        public Retreat(MinionFsm fsm) : base(fsm)
        {
            Name = "RETREAT";
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
            Minion.UpdateDestination(Transform.position - (Transform.forward * 2f),
                                     updateRotation: false);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
        }
    }
}