namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class RightHeavyWindUp : BaseWindUp
    {
        public RightHeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/WINDUP";
            nextState = "RIGHT/HEAVY/SWING";
            totalTime = 0.4f; //this will come after lightWindUp
            Animation = "RightWindup";
        }
    }
    public class RightHeavySwing : BaseSwing
    {
        public RightHeavySwing(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/SWING";
            nextState = "RIGHT/HEAVY/RECOVER";
            totalTime = 0.3f;
            Damage = 2;
            Animation = "RightSwing";
            nextStance = SwordStance.Left;
         }
    }
    public class RightHeavyRecover : BaseRecover
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

