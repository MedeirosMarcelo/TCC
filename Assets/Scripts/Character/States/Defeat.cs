using UnityEngine;

namespace Assets.Scripts.Character.States
{
    public class Defeat : CharacterState
    {
        public TimerBehaviour timer { get; protected set; }
        public AnimationBehaviour animation { get; protected set; }
        public Defeat(CharacterFsm fsm) : base(fsm)
        {
            Name = "DEFEAT";
            turnRate = 0f;

            timer = new TimerBehaviour(this);
            timer.TotalTime = 3.067f;
            timer.OnFinish = () => Fsm.ChangeState("END");

            animation = new AnimationBehaviour(this, Character.Animator);
            animation.TotalTime = 3.067f;
            animation.PlayTime = 3.067f;
            animation.Name = "Defeat";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
           AudioManager.Play(ClipType.Dead, Character.Audio);
        }

        public override void OnTriggerEnter(Collider collider) { }
    }
}