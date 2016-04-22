namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public abstract class BaseRecover : AnimatedState
    {
        const float LookTurnRate = 0.25f;
        const float LockTurnRate = 0.15f;
        public BaseRecover(CharFsm fsm) : base(fsm)
        {
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.Look(LookTurnRate, LockTurnRate);
            Character.SwordTrail.Deactivate();
        }
    }
}