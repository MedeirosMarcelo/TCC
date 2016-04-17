namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class LeftHeavyWindUp : AttackWindUp
    {
        public LeftHeavyWindUp(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            nextState = "LEFT/HEAVY/SWING";
            totalTime = 0.3f;
            Animation = "Windup";
        }
    }
    public class LeftHeavySwing : AttackSwing
    {
        public LeftHeavySwing(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            nextState = "LEFT/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "Swing";
        }
    }
    public class LeftHeavyRecover : AttackRecover
    {
        public LeftHeavyRecover(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "Recover";
        }
    }
}
