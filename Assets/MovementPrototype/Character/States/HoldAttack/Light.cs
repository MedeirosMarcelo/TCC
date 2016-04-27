namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class LightWindUp : BaseWindUp
    {
        private bool holding;
        public LightWindUp(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK/LIGHT/WINDUP";
            totalTime = 0.25f;
            Animation = "RightWindup";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding && Character.input.attack == false)
            {
                holding = false;
                nextState = "HATTACK/LIGHT/SWING";
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = "HATTACK/HEAVY/WINDUP"; // holding attack leads to a heavy
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

    public class LightSwing : BaseSwing
    {
        public LightSwing(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK/LIGHT/SWING";
            nextState = "HATTACK/LIGHT/RECOVER";
            totalTime = 0.2f;
            Animation = "RightSwing";
            Damage = 1;
        }
    }

    public class LightRecover : BaseRecover
    {
        public LightRecover(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "RightRecover";
        }
    }
}

