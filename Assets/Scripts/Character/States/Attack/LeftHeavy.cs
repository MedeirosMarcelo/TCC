namespace Assets.Scripts.Character.States.Attack
{
    public class LeftHeavyWindUp : BaseWindUp
    {
        public LeftHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            nextState = "LEFT/HEAVY/SWING";
            totalTime = 0.35f; //this will come after lightWindUp
            Animation = "LeftWindup";
        }
    }
    public class LeftHeavySwing : BaseSwing
    {
        public LeftHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            nextState = "LEFT/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "LeftSwing";
            nextStance = SwordStance.Right;
         }
    }
    public class LeftHeavyRecover : BaseRecover
    {
        public LeftHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "LeftRecover";
        }
    }
}

