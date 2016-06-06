using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{
    public class LeftHeavyWindUp : BaseWindUp
    {
        public LeftHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            timer.TotalTime = 0.35f; //this will come after lightWindUp
            timer.OnFinish = () => Fsm.ChangeState("LEFT/HEAVY/SWING");
            //Animation = "LeftWindup";
        }
    }
    public class LeftHeavySwing : BaseSwing
    {
        public LeftHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/SWING";
            timer.TotalTime = 0.2f;
            timer.OnFinish = () => Fsm.ChangeState("LEFT/HEAVY/RECOVER");
            //Animation = "LeftSwing";
            Damage = 2;
            Direction = AttackDirection.Horizontal;
            IsHeavy = true;
            nextStance = SwordStance.Right;
        }
    }
    public class LeftHeavyRecover : BaseRecover
    {
        public LeftHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/RECOVER";
            timer.TotalTime = 0.5f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
            //Animation = "LeftRecover";
        }
    }
}

