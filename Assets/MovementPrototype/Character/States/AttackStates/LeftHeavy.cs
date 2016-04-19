namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class LeftHeavyWindUp : AttackWindUp
    {
        public LeftHeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            nextState = "LEFT/HEAVY/SWING";
            totalTime = 0.3f;
            Animation = "LeftWindup";
        }
    }

    public class LeftHeavySwing : AttackSwing
    {
        public LeftHeavySwing(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            nextState = "LEFT/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "LeftSwing";
            nextStance = SwordStance.Right;
        }
    }

    public class LeftHeavyRecover : AttackRecover
    {
        public LeftHeavyRecover(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "LeftRecover";
        }
    }
}
