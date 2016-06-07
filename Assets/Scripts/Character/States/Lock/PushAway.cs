using UnityEngine;

namespace Assets.Scripts.Character.States.Lock
{
    public class PushAway : AnimatedState
    {
        const float speed = 1.5f;
        public PushAway(CharacterFsm fsm) : base(fsm)
        {
            Name = "LOCK/PUSHAWAY";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "PushAway";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((-Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}