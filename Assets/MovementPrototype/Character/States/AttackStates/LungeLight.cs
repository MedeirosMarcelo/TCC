namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class LungeLightWindUp : AttackWindUp
    {
        public LungeLightWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "LUNGE/LIGHT/WINDUP";
            nextState = "LUNGE/LIGHT/SWING";
            totalTime = 0.3f;
            Animation = "LungeWindup";
            Speed = 0f;
        }
    }

    public class LungeLightSwing : AttackSwing
    {
        public LungeLightSwing(CharFsm fsm) : base(fsm)
        {
            Name = "LUNGE/LIGHT/SWING";
            nextState = "LUNGE/LIGHT/RECOVER";
            totalTime = 0.2f;
            Damage = 1;
            Animation = "LungeSwing";
            Speed = 3f;
            nextStance = SwordStance.Right;
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.AttackCollider.enabled = false;
            Character.SwordTrail.Deactivate();
        }
    }

    public class LungeLightRecover : AttackRecover
    {
        public LungeLightRecover(CharFsm fsm) : base(fsm)
        {
            Name = "LUNGE/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.4f;
            Animation = "LungeRecover";
        }
    }
}
