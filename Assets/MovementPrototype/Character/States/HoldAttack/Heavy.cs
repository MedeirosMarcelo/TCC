namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class HeavyWindUp : BaseWindUp
    {
        public HeavyWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK/HEAVY/WINDUP";
            nextState = "HATTACK/HEAVY/SWING";
            totalTime = 0.4f; //this will come after lightWindUp
            Animation = "DownWindup";
        }
    }
    public class HeavySwing : BaseSwing
    {
        public HeavySwing(CharFsm fsm) : base(fsm)
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
        public HeavyRecover(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "DownRecover";
        }
    }
}

