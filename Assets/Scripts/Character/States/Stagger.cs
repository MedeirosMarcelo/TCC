using UnityEngine;

namespace Assets.Scripts.Character.States
{
    public class Stagger : AnimatedState
    {
        public float Speed { get; protected set; }

        public Stagger(CharacterFsm fsm) : base(fsm)
        {
            Name = "STAGGER";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "DownWindup";
            Speed = -1f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Character.SwordTrail.Deactivate();
            AudioManager.Play(ClipType.Block, Character.audioSource);
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * Speed) * Time.fixedDeltaTime));
        }
    }
}