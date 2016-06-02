namespace Assets.Scripts.Character.States.Dash
{
    public class DashRecover : CharacterState
    {
        public DashRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "DASH/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            turnRate = 0f;
        }
    }
}
