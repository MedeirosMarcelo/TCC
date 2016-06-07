using Assets.Scripts.Common;

namespace Assets.Scripts.Character.States.Attack
{
    public class DownHeavyWindUp : BaseWindUp
    {
        public DownHeavyWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/WINDUP";

            timer.TotalTime = 0.4f; //this will come after lightWindUp
            timer.OnFinish = () => Fsm.ChangeState("DOWN/HEAVY/SWING");

            animation.TotalTime = 1f;
            // animation should be running
        }
    }
    public class DownHeavySwing : BaseSwing
    {
        public DownHeavySwing(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/SWING";

            timer.TotalTime = 0.3f;
            timer.OnFinish = () => Fsm.ChangeState("DOWN/HEAVY/RECOVER");

            // animation should be running

            Damage = 2;
            Direction = AttackDirection.Vertical;
            IsHeavy = true;
        }
    }
    public class DownHeavyRecover : BaseRecover
    {
        public DownHeavyRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "DOWN/HEAVY/RECOVER";

            // animation should be running

            timer.TotalTime = 0.3f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
        }
    }
}

