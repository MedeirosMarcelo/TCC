namespace Assets.Scripts.Character.States
{
    public class End : CharacterState
    {
        public End(CharacterFsm fsm) : base(fsm)
        {
            Name = "END";
            turnRate = 0f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.End();
        }
    }
}