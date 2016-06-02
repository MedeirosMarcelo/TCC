namespace Assets.Scripts.Character.States.Attack
{
    public class LeftLightWindUp : BaseWindUp
    {
        private bool holding;
        public LeftLightWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/WINDUP";
            totalTime = 0.25f;
            Animation = "LeftWindup";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding && Character.input.attack == false)
            {
                holding = false;
                nextState = "LEFT/LIGHT/SWING";
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = "LEFT/HEAVY/WINDUP"; // holding attack leads to a heavy
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

    public class LeftLightSwing : BaseSwing
    {
        public LeftLightSwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/SWING";
            nextState = "LEFT/LIGHT/RECOVER";
            totalTime = 0.15f;
            Animation = "LeftSwing";
            Damage = 1;
            nextStance = SwordStance.Right;
        }
    }

    public class LeftLightRecover : BaseRecover
    {
        public LeftLightRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.25f;
            Animation = "LeftRecover";
        }
    }
}

