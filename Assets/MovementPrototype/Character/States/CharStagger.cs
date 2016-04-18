using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class DownLightStagger : AnimatedState
    {
        public DownLightStagger(CharFsm fsm) : base(fsm)
        {
            Name = "STAGGER";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "DownWindup";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Character.SwordTrail.Deactivate();
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play(Animation);
        }
    }
}