namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public interface HeavyAttack
    {

    }
    public class RightHeavyWindUp : BaseWindUp
    {
        public RightHeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/WINDUP";
            nextState = "RIGHT/HEAVY/SWING";
            totalTime = 0.35f; //this will come after lightWindUp
            Animation = "RightWindup";
        }
    }
    public class RightHeavySwing : BaseSwing, HeavyAttack
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
    public class RightHeavyRecover : BaseRecover
    {
        public RightHeavyRecover(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "RightRecover";
        }
    }
}

