namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class LeftWindUp : AttackWindUp
    {
        public LeftWindUp(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/WINDUP";
            nextState = "LEFT/LIGHT/SWING";
            totalTime = 0.2f;
            Animation = "Windup";
        }
    }
    public class LeftSwing : AttackSwing
    {
        public LeftSwing(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/SWING";
            nextState = "LEFT/LIGHT/RECOVER";
            totalTime = 0.1f;
            Damage = 1;
            Animation = "Swing";
        }
    }
    public class LeftRecover : AttackRecover
    {
        public LeftRecover(CFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "Recover";
        }
    }
}
