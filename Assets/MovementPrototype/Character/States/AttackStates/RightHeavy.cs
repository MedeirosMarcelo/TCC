namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class RightHeavyWindUp : AttackWindUp
    {
        public RightHeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/WINDUP";
            nextState = "RIGHT/HEAVY/SWING";
            totalTime = 0.3f;
            Animation = "RightWindup";
        }
    }

    public class RightHeavySwing : AttackSwing
    {
        public RightHeavySwing(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/SWING";
            nextState = "RIGHT/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "RightSwing";
            nextStance = SwordStance.Left;
        }
    }

    public class RightHeavyRecover : AttackRecover
    {
        public RightHeavyRecover(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "RightRecover";
        }
    }
}
