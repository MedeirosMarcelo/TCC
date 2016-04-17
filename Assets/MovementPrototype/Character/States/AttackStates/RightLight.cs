namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class RightWindUp : AttackWindUp 
    {
        public RightWindUp(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/WINDUP";
            nextState = "RIGHT/LIGHT/SWING";
            totalTime = 0.2f;
            Animation = "RightWindup";
        }
    }
    public class RightSwing : AttackSwing
    {
        public RightSwing(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/SWING";
            nextState = "RIGHT/LIGHT/RECOVER";
            totalTime = 0.1f;
            Animation = "RightSwing";
            Damage = 1;
        }
    }
    public class RightRecover : AttackRecover
    {
        public RightRecover(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/RECOVER";
            nextState = "MOVEMENT"; 
            totalTime = 0.2f;
            Animation = "RightRecover";
        }
    }
}
