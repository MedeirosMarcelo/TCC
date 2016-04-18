namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class LeftWindUp : AttackWindUp
    {
        public LeftWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/WINDUP";
            nextState = "LEFT/LIGHT/SWING";
            totalTime = 0.2f;
            Animation = "LeftWindup";
        }
    }
    public class LeftSwing : AttackSwing
    {
        public LeftSwing(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/SWING";
            nextState = "LEFT/LIGHT/RECOVER";
            totalTime = 0.1f;
            Damage = 1;
            Animation = "LeftSwing";
        }
    }
    public class LeftRecover : AttackRecover
    {
        public LeftRecover(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "LeftRecover";
        }
    }
}
