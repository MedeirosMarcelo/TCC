﻿namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class RightHeavyWindUp : AttackWindUp
    {
        public RightHeavyWindUp(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/WINDUP";
            nextState = "RIGHT/HEAVY/SWING";
            totalTime = 0.3f;
            Animation = "RightWindup";
        }
    }
    public class RightHeavySwing : AttackSwing
    {
        public RightHeavySwing(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/SWING";
            nextState = "RIGHT/HEAVY/RECOVER";
            totalTime = 0.2f;
            Damage = 2;
            Animation = "RightSwing";
        }
    }
    public class RightHeavyRecover : AttackRecover
    {
        public RightHeavyRecover(CFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.3f;
            Animation = "RightRecover";
        }
    }
}
