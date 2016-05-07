using UnityEngine;
namespace Assets.MovementPrototype.Character.States
{
    public class LockSwords : AnimatedState
    {
        public LockSwords(CharFsm fsm) : base(fsm)
        {
            Name = "LOCKSWORDS";
            nextState = "PUSHAWAY";
            totalTime = 0.5f;
            Animation = "LockSwords";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }

    public class PushAway : AnimatedState
    {
        const float speed = 2.5f;
        public PushAway(CharFsm fsm) : base(fsm)
        {
            Name = "PUSHAWAY";
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