﻿using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{
    public class LeftHeavyWindUp : BaseWindUp
    {
        public LeftHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";

            timer.TotalTime = 0.35f; //this will come after lightWindUp
            timer.OnFinish = () => Fsm.ChangeState("LEFT/HEAVY/SWING");

            // animation should be already playing
        }
    }
    public class LeftHeavySwing : BaseSwing
    {
        public LeftHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            nextStance = SwordStance.Right;

            timer.TotalTime = 0.2f;
            timer.OnFinish = () => Fsm.ChangeState("LEFT/HEAVY/RECOVER");

            animation.PlayTime = 0.2f;
            animation.TotalTime = 0.2f;
            animation.Name = "AttackHorizontalSwing";

            Displacement = 0.8f;

            Damage = 2;
            Direction = AttackDirection.Horizontal;
            IsHeavy = true;
        }
    }
    public class LeftHeavyRecover : BaseRecover
    {
        public LeftHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";

            timer.TotalTime = 0.5f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");

            animation.PlayTime = 0.5f;
            animation.TotalTime = 0.5f;
            animation.Name = "AttackHorizontalRecover";
        }
    }
}

