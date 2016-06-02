namespace Assets.Scripts.Character.States.Attack
{
    public abstract class BaseRecover : AnimatedState
    {
        public BaseRecover(CharacterFsm fsm) : base(fsm)
        {
            turnRate = 0f;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.SwordTrail.Deactivate();
        }
    }
}