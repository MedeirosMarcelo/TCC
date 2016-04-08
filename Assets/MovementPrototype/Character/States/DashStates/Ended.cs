namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Ended : DashState
    {
        public Ended(DashFsm fsm) : base(fsm)
        {
            Name = "ENDED";
            totalTime = 0.12f;
        }

        public override bool IsOver()
        {
            return elapsed >= totalTime;
        }
    }
}
