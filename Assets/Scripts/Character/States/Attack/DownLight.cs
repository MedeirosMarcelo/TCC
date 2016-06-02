using UnityEngine;

namespace Assets.Scripts.Character.States.Attack
{
    public class DownLightWindUp : BaseWindUp
    {
        private bool holding;
        public DownLightWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/WINDUP";
            totalTime = 0.25f;
            Animation = "DownWindup";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding && Character.input.attack == false)
            {
                holding = false;
                nextState = "DOWN/LIGHT/SWING";
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            nextState = "DOWN/HEAVY/WINDUP"; // holding attack leads to a heavy
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

    public class DownLightSwing : BaseSwing
    {
        public DownLightSwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/SWING";
            nextState = "DOWN/LIGHT/RECOVER";
            totalTime = 0.2f;
            Animation = "DownSwing";
            Damage = 1;
        }
    }

    public class DownLightRecover : BaseRecover
    {
        public DownLightRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/LIGHT/RECOVER";
            nextState = "MOVEMENT";
            totalTime = 0.2f;
            Animation = "DownRecover";
        }
    }
}

