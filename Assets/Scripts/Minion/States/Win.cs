using UnityEngine;

namespace Assets.Scripts.Minion.States
{
   public class Win : MinionState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public Win(MinionFsm fsm) : base(fsm)
        {
            Name = "WIN";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 1.367f;
            timer.OnFinish = () => Fsm.ChangeState("END");

            animation = new AnimationBehaviour(this, Minion.Animator);
            animation.TotalTime = 1.367f;
            animation.PlayTime = 1.367f;
            animation.Name = "Win";
        }
    }
}