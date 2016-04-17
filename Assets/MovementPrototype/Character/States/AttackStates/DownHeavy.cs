namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class DownHeavyWindUp : AttackWindUp
    {
        const float speed = 1f;
        public DownHeavyWindUp(CFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/WINDUP";
            nextState = "DOWN/HEAVY/SWING";
            totalTime = 0.3f;
            Animation = "DownWindup";
        }
    }
    public class DownHeavySwing : AttackSwing
    {
        const float speed = 2f;
        public DownHeavySwing(CFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/SWING";
            nextState = "DOWN/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "DownSwing";
        }
    }
    public class DownHeavyRecover : AttackRecover
    {
        public DownHeavyRecover(CFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "DownRecover";
        }
    }
}
