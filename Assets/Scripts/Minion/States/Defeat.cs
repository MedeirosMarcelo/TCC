using UnityEngine;

namespace Assets.Scripts.Minion.States
{
    public class Defeat : MinionState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public Defeat(MinionFsm fsm) : base(fsm)
        {
            Name = "DEFEAT";

            timer = new TimerBehaviour(this);
            timer.TotalTime = 3.067f;
            timer.OnFinish = () => Fsm.ChangeState("END");

            animation = new AnimationBehaviour(this, Minion.Animator);
            animation.TotalTime = 3.067f;
            animation.PlayTime = 3.067f;
            animation.Name = "Defeat";
        }
        public override void OnTriggerEnter(Collider collider) { }
        public override void OnCollisionEnter(Collision collision) { }
    }
}