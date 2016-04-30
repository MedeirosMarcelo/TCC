namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class LeftHeavyWindUp : BaseWindUp
    {
        public LeftHeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            nextState = "LEFT/HEAVY/SWING";
            totalTime = 0.4f; //this will come after lightWindUp
            Animation = "LeftWindup";
        }
    }
    public class LeftHeavySwing : BaseSwing
    {
        public LeftHeavySwing(CharFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            nextState = "LEFT/HEAVY/RECOVER";
            totalTime = 0.3f;
            Damage = 2;
            Animation = "LeftSwing";
            nextStance = SwordStance.Right;
         }
    }
    public class LeftHeavyRecover : BaseRecover
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

