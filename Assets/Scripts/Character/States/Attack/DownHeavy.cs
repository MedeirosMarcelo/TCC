namespace Assets.Scripts.Character.States.Attack
{
    public class DownHeavyWindUp : BaseWindUp
    {
        public DownHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/WINDUP";
            nextState = "DOWN/HEAVY/SWING";
            totalTime = 0.4f; //this will come after lightWindUp
            Animation = "DownWindup";
        }
    }
    public class DownHeavySwing : BaseSwing
    {
        public DownHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/SWING";
            nextState = "DOWN/HEAVY/RECOVER";
            totalTime = 0.3f;
            Damage = 2;
            Animation = "DownSwing";
         }
    }
    public class DownHeavyRecover : BaseRecover
    {
        public DownHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "DownRecover";
        }
    }
}

