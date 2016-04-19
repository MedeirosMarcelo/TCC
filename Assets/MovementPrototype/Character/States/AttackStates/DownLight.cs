namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class DownWindUp : AttackWindUp
    {
        public DownWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/WINDUP";
            nextState = "DOWN/LIGHT/SWING";
            totalTime = 0.2f;
            Animation = "DownWindup";
        }
    }

    public class DownSwing : AttackSwing
    {
        public DownSwing(CharFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/SWING";
            nextState = "DOWN/LIGHT/RECOVER";
            totalTime = 0.1f;
            Damage = 1;
            Animation = "DownSwing";
            nextStance = SwordStance.High;
        }
    }

    public class DownRecover : AttackRecover
    {
        public DownRecover(CharFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "DownRecover";
        }
    }
}
