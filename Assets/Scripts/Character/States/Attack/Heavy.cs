namespace Assets.Scripts.Character.States.Attack
{
    public class HeavyWindUp : BaseWindUp
    {
        public HeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "HATTACK/HEAVY/WINDUP";
            nextState = "HATTACK/HEAVY/SWING";
            totalTime = 0.4f; //this will come after lightWindUp
            Animation = "DownWindup";
        }
    }
    public class HeavySwing : BaseSwing
    {
        public HeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "HATTACK/HEAVY/SWING";
            nextState = "HATTACK/HEAVY/RECOVER";
            totalTime = 0.3f;
            Damage = 2;
            Animation = "DownSwing";
         }
    }
    public class HeavyRecover : BaseRecover
    {
        public HeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "HATTACK/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "DownRecover";
        }
    }
}

