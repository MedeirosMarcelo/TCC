namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class RightLightWindUp : BaseWindUp
    {
        private bool holding;
        public RightLightWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/WINDUP";
            totalTime = 0.25f;
            Animation = "RightWindup";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding && Character.input.attack == false)
            {
                holding = false;
                nextState = "RIGHT/LIGHT/SWING";
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = "RIGHT/HEAVY/WINDUP"; // holding attack leads to a heavy
            holding = true;
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            // if (holding)
            // {
            //    TODO: Add visual feedback  
            //    UnityEngine.Debug.Log("Promoted to Heavy!");
            // }

        }
    }

    public class RightLightSwing : BaseSwing
    {
        public RightLightSwing(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/SWING";
            nextState = "RIGHT/LIGHT/RECOVER";
            totalTime = 0.15f;
            Animation = "RightSwing";
            Damage = 1;
            nextStance = SwordStance.Left;
        }
    }

    public class RightLightRecover : BaseRecover
    {
        public RightLightRecover(CharFsm fsm) : base(fsm)
        {
            Name = "RIGHT/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.25f;
            Animation = "RightRecover";
        }
    }
}

