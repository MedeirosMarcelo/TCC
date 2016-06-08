using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{
    public class LeftLightWindUp : BaseWindUp
    {
        private bool holding;
        public LeftLightWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/WINDUP";

            timer.TotalTime = 0.25f;
            timer.OnFinish = () => Fsm.ChangeState(holding ? "LEFT/HEAVY/WINDUP" : "LEFT/LIGHT/SWING");

            animation.PlayTime = 0.6f; // Windup includes Heavy 
            animation.TotalTime = 0.6f;
            animation.Name = "AttackHorizontalWindup";
        }
        public override void PreUpdate()
        {
            base.PreUpdate();
            if (holding && Character.input.attack == false)
            {
                holding = false;
            }
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
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
            nextStance = SwordStance.Right;

            timer.TotalTime = 0.15f;
            timer.OnFinish = () => Fsm.ChangeState("LEFT/LIGHT/RECOVER");

            animation.TotalTime = 0.2f;
            animation.PlayTime = 0.15f;
            animation.Name = "AttackHorizontalSwing";

            Displacement = 0.8f;

            Damage = 1;
            Direction = AttackDirection.Horizontal;
            IsHeavy = false;
        }
    }

    public class LeftLightRecover : BaseRecover
    {
        public LeftLightRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/RECOVER";

            timer.TotalTime = 0.25f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");

            animation.TotalTime = 0.5f;
            animation.PlayTime = 0.25f;
            animation.Name = "AttackHorizontalRecover";
        }
    }
}

