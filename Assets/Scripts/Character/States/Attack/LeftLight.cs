using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Character.States.Attack
{
    public class LeftLightWindUp : BaseWindUp
    {
        private bool holding;
        public LeftLightWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/WINDUP";
            timer.OnFinish = () => Fsm.ChangeState(holding ? "LEFT/HEAVY/WINDUP" : "LEFT/LIGHT/SWING");
            timer.TotalTime = 0.25f;
            //Animation = "LeftWindup";
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
            timer.TotalTime = 0.15f;
            timer.OnFinish = () => Fsm.ChangeState("LEFT/LIGHT/RECOVER");
            //Animation = "LeftSwing";
            Damage = 1;
            Direction = AttackDirection.Horizontal;
            IsHeavy = false;
            nextStance = SwordStance.Right;
        }
    }

    public class LeftLightRecover : BaseRecover
    {
        public LeftLightRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/LIGHT/RECOVER";
            timer.TotalTime = 0.25f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
            //Animation = "LeftRecover";
        }
    }
}

