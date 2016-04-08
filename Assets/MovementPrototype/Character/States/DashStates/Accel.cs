namespace Assets.MovementPrototype.Character.States.DashStates
{
    public class Accel : DashState
    {
        public Accel(DashFsm fsm) : base(fsm)
        {
            Name = "ACCEL";
        }
    }
}
