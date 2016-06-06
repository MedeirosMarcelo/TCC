using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{
    public class RightHeavyWindUp : BaseWindUp
    {
        public RightHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/WINDUP";
            timer.TotalTime = 0.35f; //this will come after lightWindUp
            timer.OnFinish = () => Fsm.ChangeState("RIGHT/HEAVY/SWING");
            //Animation = "RightWindup";
        }
    }
    public class RightHeavySwing : BaseSwing
    {
        public RightHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/SWING";
            timer.TotalTime = 0.2f;
            timer.OnFinish = () => Fsm.ChangeState("RIGHT/HEAVY/RECOVER");
            //Animation = "RightSwing";
            Damage = 2;
            Direction = AttackDirection.Horizontal;
            IsHeavy = true;
            nextStance = SwordStance.Left;
         }
    }
    public class RightHeavyRecover : BaseRecover
    {
        public RightHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "RIGHT/HEAVY/RECOVER";
            timer.TotalTime = 0.5f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
            //Animation = "RightRecover";
        }
    }
}

