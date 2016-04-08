namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Deccel : DashState
    {
        public Deccel(DashFsm fsm) : base(fsm)
        {
            Name = "DECCEL";
        }
    }
}
