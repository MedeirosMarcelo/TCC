public abstract class AttackRecover : AnimatedState
{
    public AttackRecover(CFsm fsm) : base(fsm)
    {
    }
    public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.Look(0.25f, 0.15f);
        Character.SwordTrail.Deactivate();
    }
}
