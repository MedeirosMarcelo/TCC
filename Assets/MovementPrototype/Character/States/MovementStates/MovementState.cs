namespace Assets.MovementPrototype.Character.States.MovementStates
{
    public class MovementState : ProxyState
    {
        public MovementState(CharFsm fsm) : base(fsm)
        {
            Name = "MOVEMENT";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            if (Character.input.run >= 0.25f)
            {
                Character.fsm.ChangeState("MOVEMENT/RUN");
            }
            else
            {
                Character.fsm.ChangeState("MOVEMENT/LOCK");
            }
        }
    }
}