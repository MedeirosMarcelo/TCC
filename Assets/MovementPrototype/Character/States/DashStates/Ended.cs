namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Ended : DashState
    {
        public Ended(DashFsm fsm) : base(fsm)
        {
            Name = "ENDED";
        }
    }
}
